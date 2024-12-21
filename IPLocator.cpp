#include <iostream>
#include <string>
#include <curl/curl.h>

size_t WriteCallback(void* contents, size_t size, size_t nmemb, void* userp) {
    ((std::string*)userp)->append((char*)contents, size * nmemb);
    return size * nmemb;
}

void get_geolocation(const std::string& ip) {
    CURL* curl;
    CURLcode res;
    std::string readBuffer;

    curl = curl_easy_init();
    if(curl) {
        curl_easy_setopt(curl, CURLOPT_URL, ("http://ip-api.com/json/" + ip).c_str());
        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteCallback);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);
        res = curl_easy_perform(curl);
        curl_easy_cleanup(curl);

        if(res == CURLE_OK) {
            std::cout << "Data: " << readBuffer << std::endl;
        } else {
            std::cerr << "Erreur lors de la requête" << std::endl;
        }
    }
}

int main() {
    std::string ip;
    std::cout << "Veuillez entrer l'adresse IP: ";
    std::cin >> ip;
    get_geolocation(ip);
    std::cout << "Appuyez sur Entrée pour quitter..." << std::endl;
    std::cin.get();
    std::cin.get();
    return 0;
}