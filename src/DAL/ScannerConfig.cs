using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class ScannerConfig
    {
        public static DAL.DTO.ScannerConfig getScannerConfig(string deviceId)
        {
            using (var db = new DAL.Models.AISContext())
            {
                var config = db.ScannerConfigs.FirstOrDefault(x => x.DeviceId.Equals(deviceId));
                if (config == null) return null;
                return ToDTO(config);
            }
        }

        public static DAL.DTO.ScannerConfig setScannerLocation(DAL.Models.ScannerConfig values)
        {
            using (var db = new DAL.Models.AISContext())
            {
                var model = new DAL.Models.ScannerConfig();

                if (db.ScannerConfigs.FirstOrDefault(x => x.DeviceId.Equals(values.DeviceId)) != null)
                {
                    return editScannerLocation(values.DeviceId, values);
                }
                else
                {
                    model = db.ScannerConfigs.Add(values).Entity;
                    db.SaveChanges();
                }
                return ToDTO(model);
            }
        }

        public static DAL.DTO.ScannerConfig editScannerLocation(string deviceId, DAL.Models.ScannerConfig scannerConfig)
        {
            using var db = new DAL.Models.AISContext();
            var config = db.ScannerConfigs.FirstOrDefault(x => x.DeviceId == deviceId);
            if (config == null) throw new ArgumentException(nameof(config));

            config.PlantLocationId = scannerConfig.PlantLocationId;
            config.DeviceId = scannerConfig.DeviceId;
            config.DeviceName = scannerConfig.DeviceName;
            db.ScannerConfigs.Update(config);
            db.SaveChanges();
            return ToDTO(config);
        }

        public static DAL.DTO.ScannerConfig ToDTO(DAL.Models.ScannerConfig scannerConfig)
        {
            return new DAL.DTO.ScannerConfig
            {
                DeviceId = scannerConfig.DeviceId,
                DeviceName = scannerConfig.DeviceName,
                PlantLocationId = scannerConfig.PlantLocationId,
            };
        }
    }
}