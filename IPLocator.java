import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class Main {
    public static void main(String[] args) throws Exception {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        System.out.print("Veuillez entrer l'adresse IP: ");
        String ip = reader.readLine();
        getGeolocation(ip);
        System.out.println("Appuyez sur Entrée pour quitter...");
        reader.readLine();
    }

    public static void getGeolocation(String ip) throws Exception {
        String url = "http://ip-api.com/json/" + ip;
        HttpURLConnection connection = (HttpURLConnection) new URL(url).openConnection();
        connection.setRequestMethod("GET");

        BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
        String inputLine;
        StringBuffer content = new StringBuffer();
        while ((inputLine = in.readLine()) != null) {
            content.append(inputLine);
        }
        in.close();

        if (connection.getResponseCode() == 200) {
            System.out.println("Data: " + content.toString());
        } else {
            System.out.println("Adresse IP non trouvée ou invalide.");
        }
    }
}