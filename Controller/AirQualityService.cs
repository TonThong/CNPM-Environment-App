using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Environmental_Monitoring.Controller
{
    public class AirQualityService
    {
        // THAY API KEY CỦA BẠN VÀO ĐÂY (Đăng ký tại openweathermap.org)
        private const string API_KEY = "edb0674d601ce28d0d20f669b660227e"; 

        private static readonly HttpClient client = new HttpClient();

        // Danh sách tọa độ 34 tỉnh thành lớn (Latitude, Longitude)
        public static readonly Dictionary<string, (double Lat, double Lon)> Provinces = new Dictionary<string, (double, double)>
        {
            {"Hà Nội", (21.0285, 105.8542)}, {"TP. Hồ Chí Minh", (10.8231, 106.6297)},
            {"Đà Nẵng", (16.0544, 108.2022)}, {"Hải Phòng", (20.8449, 106.6881)},
            {"Cần Thơ", (10.0452, 105.7469)}, {"An Giang", (10.5216, 105.1259)},
            {"Bà Rịa - Vũng Tàu", (10.4114, 107.1362)}, {"Bắc Giang", (21.2731, 106.1946)},
            {"Bắc Ninh", (21.1861, 106.0763)}, {"Bình Dương", (11.1600, 106.6600)},
            {"Bình Định", (13.9776, 109.1363)}, {"Bình Thuận", (11.0939, 108.0452)},
            {"Cà Mau", (9.1769, 105.1524)}, {"Đắk Lắk", (12.6667, 108.0500)},
            {"Đồng Nai", (11.0537, 107.0509)}, {"Gia Lai", (13.9833, 108.0000)},
            {"Hà Giang", (22.8233, 104.9836)}, {"Hà Nam", (20.5429, 105.9155)},
            {"Hà Tĩnh", (18.3424, 105.9058)}, {"Hải Dương", (20.9378, 106.3146)},
            {"Hòa Bình", (20.8133, 105.3383)}, {"Hưng Yên", (20.6464, 106.0511)},
            {"Khánh Hòa", (12.2451, 109.1943)}, {"Kiên Giang", (10.0119, 105.0808)},
            {"Lâm Đồng", (11.9404, 108.4583)}, {"Lạng Sơn", (21.8533, 106.7614)},
            {"Lào Cai", (22.4856, 103.9707)}, {"Nam Định", (20.4234, 106.1684)},
            {"Nghệ An", (19.2333, 104.9500)}, {"Ninh Bình", (20.2539, 105.9750)},
            {"Phú Thọ", (21.3167, 105.2000)}, {"Quảng Ninh", (21.0069, 107.2925)},
            {"Thái Nguyên", (21.5942, 105.8482)}, {"Thanh Hóa", (19.8067, 105.7851)}
        };

        public static async Task<PollutionResponse> GetPollutionForecastAsync(double lat, double lon)
        {
            // Gọi API lấy dự báo ô nhiễm (Forecast)
            string url = $"http://api.openweathermap.org/data/2.5/air_pollution/forecast?lat={lat}&lon={lon}&appid={API_KEY}";

            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<PollutionResponse>(response);
        }
    }

    // Các class để hứng dữ liệu JSON
    public class PollutionResponse
    {
        public List<PollutionData> list { get; set; }
    }

    public class PollutionData
    {
        public long dt { get; set; } // Thời gian (Unix timestamp)
        public MainInfo main { get; set; }
        public ComponentsInfo components { get; set; }
    }

    public class MainInfo
    {
        public int aqi { get; set; } // Chỉ số AQI (1 = Tốt, 5 = Rất Xấu)
    }

    public class ComponentsInfo
    {
        public double pm2_5 { get; set; } // Bụi mịn PM2.5
        public double co { get; set; }    // Khí CO
    }
}