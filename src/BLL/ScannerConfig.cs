using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class ScannerConfig
    {
        public static DAL.DTO.ScannerConfig getScannerConfig(string deviceId)
        {
            return DAL.ScannerConfig.getScannerConfig(deviceId);
        }

        public static DAL.DTO.ScannerConfig setScannerLocation(DAL.Models.ScannerConfig values)
        {
            return DAL.ScannerConfig.setScannerLocation(values);
        }

        public static DAL.DTO.ScannerConfig editScannerLocation(string deviceId, DAL.Models.ScannerConfig values)
        {
            return DAL.ScannerConfig.editScannerLocation(deviceId, values);
        }
    }
}