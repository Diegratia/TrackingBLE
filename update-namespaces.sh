#!/bin/bash
# File: update-namespaces.sh

# Root namespace yang diinginkan
ROOT_NS="TrackingBle.src"

# Fungsi untuk memperbarui namespace dalam folder tertentu
update_namespace() {
  local folder="$1"
  local proj_name=$(basename "$folder" | tr '.' '_')  # Ganti titik dengan underscore
  local base_ns="$ROOT_NS.$proj_name"

  # Cari semua file .cs di folder
  find "$folder" -name "*.cs" | while read -r file; do
    # Hitung subfolder relatif terhadap folder proyek
    subfolder=$(dirname "$file" | sed "s|$folder||g" | tr '/' '.' | sed 's/^.//')
    new_ns="$base_ns${subfolder:+.$subfolder}"

    # Perbarui namespace di file
    if grep -q "namespace" "$file"; then
      sed -i '' "s/namespace .*/namespace $new_ns/" "$file"
      echo "Updated $file to $new_ns"
    else
      # Tambahkan namespace jika belum ada
      sed -i '' "1i\\
namespace $new_ns\\
{" "$file"
      echo "Added namespace $new_ns to $file"
      echo "}" >> "$file"
    fi
  done
}

# Perbarui namespace untuk semua proyek di src/
for proj in src/*; do
  if [ -d "$proj" ]; then
    update_namespace "$proj"
  fi
done

echo "Namespace update completed!"

