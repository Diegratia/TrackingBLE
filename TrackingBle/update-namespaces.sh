#!/bin/bash
# File: update-namespaces.sh

# Root namespace yang diinginkan
ROOT_NS="TrackingBle.src"

update_namespace() {
  local folder="$1"
  # Ambil nama folder dan ubah menjadi format _X.Nama (misalnya, 7.MstApplication → _7.MstApplication)
  local proj_name=$(basename "$folder" | sed 's/^\([0-9]\+\)\.\(.*\)$/_ \1.\2/' | tr ' ' '_')
  local base_ns="$ROOT_NS.$proj_name"

  echo "Processing folder: $folder"
  echo "Base namespace: $base_ns"

  # Cari file .cs dan pastikan ada hasil
  local files=$(find "$folder" -name "*.cs" 2>/dev/null)
  if [ -z "$files" ]; then
    echo "No .cs files found in $folder"
    return
  fi

  echo "Found files:"
  echo "$files"

  # Proses setiap file .cs
  find "$folder" -name "*.cs" 2>/dev/null | while read -r file; do
    # Hitung subfolder relatif terhadap folder proyek
    subfolder=$(dirname "$file" | sed "s|$folder||g" | tr '/' '.' | sed 's/^\.//')
    new_ns="$base_ns${subfolder:+.$subfolder}"

    echo "Checking $file for namespace..."

    # Cek apakah file sudah memiliki namespace
    if grep -q "^namespace" "$file"; then
      # Ganti namespace yang ada dengan yang baru
      sed -i '' "s/^namespace .*/namespace $new_ns/" "$file"
      echo "Updated $file to $new_ns"
    else
      # Tambahkan namespace jika tidak ada
      tmp_file=$(mktemp)
      echo "namespace $new_ns" > "$tmp_file"
      echo "{" >> "$tmp_file"
      cat "$file" >> "$tmp_file"
      echo "}" >> "$tmp_file"
      mv "$tmp_file" "$file"
      echo "Added namespace $new_ns to $file"
    fi
  done
}

# Perbarui namespace untuk semua proyek di src/
echo "Starting namespace update..."
for proj in src/*; do
  if [ -d "$proj" ]; then
    update_namespace "$proj"
  fi
done

echo "Namespace update completed!"
