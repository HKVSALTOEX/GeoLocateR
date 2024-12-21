import requests

def get_geolocation(ip):
    try:
        response = requests.get(f'http://ip-api.com/json/{ip}')
        response.raise_for_status()
        data = response.json()
        return data
    except requests.RequestException as e:
        print(f"Erreur lors de la requête : {e}")
        return None

def main():
    ip = input("Veuillez entrer l'adresse IP: ")
    location = get_geolocation(ip)
    if location and location['status'] == 'success':
        print(f"Pays: {location['country']}\nRégion: {location['regionName']}\nVille: {location['city']}\nLatitude: {location['lat']}\nLongitude: {location['lon']}")
    else:
        print("Adresse IP non trouvée ou invalide.")
    
    input("Appuyez sur Entrée pour quitter...")

if __name__ == "__main__":
    main()