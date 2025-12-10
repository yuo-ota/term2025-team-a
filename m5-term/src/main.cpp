#include <Arduino.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include <M5Stack.h>

// WiFi設定
const char *ssid = "yourssid";
const char *password = "yourPASSWORD";

// mockData
float temp = 25.5;
float humidity = 60.0;
String deviceId = "Device123";

const char *serverUrl = "http://[IPAdress]:[PortNumber]/api/m5data";

void setup()
{
    M5.begin();
    Serial.begin(115200);
    WiFi.begin(ssid, password);


    while (WiFi.status() != WL_CONNECTED)
    {
        delay(1000);
        M5.Lcd.printf("Connecting to WiFi...");
    }
    Serial.println("Connected to WiFi");
    M5.Lcd.printf("Connected to WiFi\n");
}

void loop()
{
    StaticJsonDocument<200> doc;

    doc["device_id"] = deviceId;
    doc["temperature"] = temp;
    doc["humidity"] = humidity;

    String jsonpayload;
    serializeJson(doc, jsonpayload);

    HTTPClient http;
    http.begin(serverUrl);

    http.addHeader("Content-Type", "application/json");

    int httpResponseCode = http.POST(jsonpayload);

    if (httpResponseCode > 0)
    {
        Serial.print("HTTP Response code: ");
        M5.Lcd.printf("HTTP Response code: %d\n", httpResponseCode);
        Serial.println(httpResponseCode);
        String response = http.getString();
        Serial.println(response);
        M5.Lcd.println(response);
    }
    else
    {
        Serial.print("Error code: ");
        Serial.println(httpResponseCode);
        M5.Lcd.printf("Error code: %d\n", httpResponseCode);
    }
    http.end();
    delay(10000); // 10秒待機
}
