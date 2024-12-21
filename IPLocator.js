const http = require('http');

function getGeolocation(ip) {
    return new Promise((resolve, reject) => {
        http.get(`http://ip-api.com/json/${ip}`, (res) => {
            let data = '';

            res.on('data', (chunk) => {
                data += chunk;
            });

            res.on('end', () => {
                const location = JSON.parse(data);
                resolve(location);
            });
        }).on('error', (err) => {
            reject(err);
        });
    });
}

async function main() {
    const readline = require('readline').createInterface({
        input: process.stdin,
        output: process.stdout
    });

    readline.question('Veuillez entrer l\'adresse IP: ', async (ip) => {
        try {
            const location = await getGeolocation(ip);
            if (location.status === 'success') {
                console.log(`Pays: ${location.country}\nRégion: ${location.regionName}\nVille: ${location.city}\nLatitude: ${location.lat}\nLongitude: ${location.lon}`);
            } else {
                console.log('Adresse IP non trouvée ou invalide.');
            }
        } catch (error) {
            console.error('Erreur lors de la requête:', error);
        }

        console.log('Appuyez sur Entrée pour quitter...');
        readline.close();
    });
}

main();