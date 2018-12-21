using Google.OpenLocationCode;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;


namespace OpenLocationCodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //global codes, such as "796RWF8Q+WF"
            //local codes, such as "WF8Q+WF"
            //local codes with a locality, such as "WF8Q+WF Praia, Cabo Verde"


            //グローバルコードはそのままいける
            var locationCode = "8Q9WCVW2+JFJ";
            var olc = new OpenLocationCode(locationCode);//グローバルコードを入れれば、その経緯度をローカルで計算できる。
            var decoded = olc.Decode();

            Console.WriteLine($"GlobalCode:::lat:{decoded.CenterLatitude}, lon:{decoded.CenterLongitude}");

            //ローカルコードは基準となる経緯度をもとに、計算する。
            locationCode = "CVW2+JFJ";
            olc = new OpenLocationCode(locationCode);

            //基準となる経緯度はローカルコードが指す場所の比較的近くである必要がある。
            //37.4614948,-23.5037148
            //デバイスのGPS情報を使えば、自分の周囲の位置を指す経緯度を計算することができる。
            double lat = 14.9218848;//緯度:南北
            double lon = 138.7775175;//経度：東西
            var recoverdOlc = olc.Recover(lat, lon);//基準となる経緯度を指定してグルーバルコードのOpenLocationCodeオブジェクトを取得する。
            decoded = recoverdOlc.Decode();

            Console.WriteLine($"LocalCode:::lat:{decoded.CenterLatitude}, lon:{decoded.CenterLongitude}");


            //ショートコード＋都市（場所名）は処理できない。
            //Google Map ApiというかGoogle Geocoode apiで経緯度を求める必要がある。
            try
            {
                locationCode = "CVW2+JFJ Niigata,Nagaoka";
                olc = new OpenLocationCode(locationCode); //これはできない。
                decoded = olc.Decode();

                Console.WriteLine($"LocalCodeWithCity:::lat:{decoded.CenterLatitude}, lon:{decoded.CenterLongitude}");
            }
            catch
            {
                //例外を握りつぶす
            }



            //GETメソッドで
            using (var httpClient = new HttpClient())
            {
                var uri = $"https://plus.codes/api?address=CVW2%2BJFJ%20Niigata,Nagaoka&ekey=＜あなたのキー＞";

                var responseString = httpClient.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;

                Console.WriteLine(responseString);
            }

            //POSTメソッドで
            using (var httpClient = new HttpClient())
            {
                var httpRequest = new FormUrlEncodedContent(
                    new Dictionary<string, string>()
                    {
                        {"address","CVW2+JFJ Niigata,Nagaoka" },
                        {"ekey","＜あなたのキー＞" },
                    }
                );

                var httpResponse = httpClient.PostAsync("https://plus.codes/api", httpRequest).Result;

                //Jsonをデシリアライズ
                var response = httpResponse.Content.ReadAsStreamAsync().Result;

                var jsonSerializer = new DataContractJsonSerializer(typeof(PlusCodeResponse));
                var responseJson = jsonSerializer.ReadObject(response) as PlusCodeResponse;


                lat = responseJson.PlusCode.Geometry.Location.Lat;
                lon = responseJson.PlusCode.Geometry.Location.Lng;


                Console.WriteLine($"Response:::lat:{lat}, lon:{lon}");


                //Jsonをそのまま出力
                var responseString = httpResponse.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseString);

            }


            Console.ReadKey();
        }
    }
}
