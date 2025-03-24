using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingBle.src._3FloorplanDevice.Models.Domain
{

    public class FloorplanDevice
    {
        [Column("_generate")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Generate { get; set; }

        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid(); 

        [Required]
        [StringLength(255)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("type")]
        public DeviceType Type { get; set; }

        [Required]
        [ForeignKey("Floorplan")]
        [Column("floorplan_id")]
        public Guid FloorplanId { get; set; }

        [Required]
        [ForeignKey("AccessCctv")]
        [Column("access_cctv_id")]
        public Guid AccessCctvId { get; set; }

        [Required]
        [ForeignKey("Reader")]
        [Column("ble_reader_id")]
        public Guid ReaderId { get; set; }

        [Required]
        [ForeignKey("AccessControl")]
        [Column("access_control_id")]
        public Guid AccessControlId { get; set; }

        [Required]
        [Column("pos_x")]
        public decimal PosX { get; set; }

        [Required]
        [Column("pos_y")]
        public decimal PosY { get; set; }

        [Required]
        [Column("pos_px_x")]
        public long PosPxX { get; set; }

        [Required]
        [Column("pos_px_y")]
        public long PosPxY { get; set; }

        [Required]
        [ForeignKey("FloorplanMaskedArea")]
        [Column("floorplan_masked_area_id")]
        public Guid FloorplanMaskedAreaId { get; set; }

        [Required]
        [ForeignKey("Application")]
        [Column("application_id")]
        public Guid ApplicationId { get; set; }

        [Required]
        [StringLength(255)]
        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(255)]
        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        [Required]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Required]
        [Column("device_status")]
        public DeviceStatus DeviceStatus { get; set; }

        [Required]
        [Column("status")]
        public int? Status { get; set; } = 1;

        // public virtual MstFloorplan Floorplan { get; set; }
        // public virtual MstAccessCctv AccessCctv { get; set; }
        // public virtual MstBleReader Reader { get; set; }
        // public virtual MstAccessControl AccessControl { get; set; }
        // public virtual FloorplanMaskedArea FloorplanMaskedArea { get; set; }
        // public virtual MstApplication Application { get; set; }
    }

         public enum DeviceType
    {
        Cctv,
        AccessDoor,
        BleReader
    }

       public enum DeviceStatus
    {
        Active,
        NonActive,
        Damaged,
        Close,
        Open,
        Monitor,
        Alarm
    }
}
