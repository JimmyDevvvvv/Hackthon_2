using UnityEngine;
using System;  // Added this line for the Convert class
using System.Security.Cryptography;
using System.Text;

public class WiFiNetwork : MonoBehaviour
{
    // Enum to represent different network types
    public enum NetworkType
    {
        WPA2_Personal,
        WPA3_Personal,
        WPA2_Enterprise,
        WPA3_Enterprise,
        WEP,
        OpenWiFi,
        WPA_Mixed_Mode,
        GuestWiFi,
        MeshNetwork,
        WiFi6,
        WiFi5,
        AdHoc,
        Hotspot
    }

    public NetworkType networkType;
    public bool isSecureNetwork; // True for secure networks, false for insecure ones

    // For floating text and data transfer
    public GameObject dataLinePrefab; // The line that represents data transfer
    private GameObject dataLine; // Instance of the data transfer line

    // Placeholder for username (this could be dynamically set based on the player's login or input)
    public string username = "JohnDoe";  // Example username, this can be modified or passed dynamically

    // AES Encryption key (32 bytes for AES-256)
    private static readonly byte[] aesKey = Encoding.UTF8.GetBytes("1234567890abcdef1234567890abcdef");
    private static readonly byte[] aesIV = Encoding.UTF8.GetBytes("abcdef9876543210");

    // Start the data transfer when the network is selected
    public void StartDataTransfer()
    {
        // Create the data line that represents the transfer
        dataLine = Instantiate(dataLinePrefab, transform.position, Quaternion.identity);

        // Adjust the network security (visual effect) and data transfer
        string dataToSend = "username:" + username + " password:HowUrCredsGetTransfered";

        switch (networkType)
        {
            case NetworkType.WPA2_Personal:
                DataVisualization(EncryptAES(dataToSend), Color.green);
                break;
            case NetworkType.WPA3_Personal:
                DataVisualization(EncryptAES(dataToSend), Color.blue);
                break;
            case NetworkType.WPA2_Enterprise:
                DataVisualization(EncryptAES(dataToSend), Color.cyan);
                break;
            case NetworkType.WPA3_Enterprise:
                DataVisualization(EncryptAES(dataToSend), Color.blue);
                break;
            case NetworkType.WEP:
                DataVisualization(HashSHA256(dataToSend), Color.red);
                break;
            case NetworkType.OpenWiFi:
                DataVisualization(HashSHA256(dataToSend), Color.white);
                break;
            case NetworkType.WPA_Mixed_Mode:
                DataVisualization(EncryptAES(dataToSend), Color.yellow);
                break;
            case NetworkType.GuestWiFi:
                DataVisualization(EncryptAES(dataToSend), new Color(1f, 0.647f, 0f)); // Orange (manually defined)
                break;
            case NetworkType.MeshNetwork:
                DataVisualization(EncryptAES(dataToSend), Color.green);
                break;
            case NetworkType.WiFi6:
                DataVisualization(EncryptAES(dataToSend), Color.green);
                break;
            case NetworkType.WiFi5:
                DataVisualization(EncryptAES(dataToSend), Color.blue);
                break;
            case NetworkType.AdHoc:
                DataVisualization(HashSHA256(dataToSend), Color.gray);
                break;
            case NetworkType.Hotspot:
                DataVisualization(HashSHA256(dataToSend), new Color(0.5f, 0f, 0.5f)); // Purple (manually defined)
                break;
            default:
                break;
        }
    }

    // AES Encryption (simulating WPA2 and WPA3 encryption)
    private string EncryptAES(string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = aesKey;
            aesAlg.IV = aesIV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            byte[] buffer = Encoding.UTF8.GetBytes(plainText);

            byte[] encrypted = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
            return Convert.ToBase64String(encrypted);
        }
    }

    // SHA-256 Hashing (simulating WEP and Open Wi-Fi encryption)
    private string HashSHA256(string plainText)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    // Method to set up the data visualization based on network security
    private void DataVisualization(string data, Color dataColor)
    {
        DataTransfer dataTransfer = dataLine.GetComponent<DataTransfer>();
        dataTransfer.SetData(data, dataColor);
        dataTransfer.StartDataTransfer();
    }

    void OnMouseDown()
    {
        StartDataTransfer();  // Start data transfer when the sphere is clicked
    }


}
