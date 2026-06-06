#include <Arduino.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
// #include <M5Core2.h>
#include "secrets.h"
#include <M5Unified.h>
#include <M5UnitENV.h>

//ENV4設定
SHT4X sht4x;

// data_types
float temp;
float humidity;
String deviceId = "Device123";

void setup()
{
    M5.begin();
    Serial.begin(115200);
    
    WiFi.begin(ssid, password);

    M5.Lcd.printf("Connecting to WiFi...\n");
    while (WiFi.status() != WL_CONNECTED)
    {
        delay(1000);
        M5.Lcd.printf(".");
    }
    Serial.println("Connected to WiFi");
    M5.Lcd.printf("Connected to WiFi\n");

    //EMV Initialization
    if(!sht4x.begin(&Wire, SHT40_I2C_ADDR_44, 32, 33, 400000)){
        M5.Lcd.printf("SHT4x not found!");
    }else{
        M5.Lcd.printf("SHT4s found!");
    }
}

void loop()
{
    M5.update();
    // ENV4 get data
    if (sht4x.update()) {
        temp = sht4x.cTemp;
        humidity = sht4x.humidity;
        Serial.printf("Temp: %.2f C, Humidity: %.2f %%\n", temp, humidity);
    }
    
    DynamicJsonDocument doc(200);

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