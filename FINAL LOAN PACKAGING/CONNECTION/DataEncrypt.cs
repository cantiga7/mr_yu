using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

class DataEncrypt
{

    //  Call this function to remove the key from memory after use for security
    [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
    public static extern bool ZeroMemory(IntPtr Destination, int Length);


    static readonly string PasswordHash = "B1@dmin_$Upp0rt";
    static readonly string SaltKey = "S@LT&KEY";
    static readonly string VIKey = "@1B2c3D4e5F6g7H8";


    // Function to Generate a 64 bits Key.
    public static string GenerateKey()
    {
        // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
        DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

        // Use the Automatically generated key for Encryption. 
        return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
    }


    //private static void EncryptFile(string inputFile, string outputFile, string skey)
    //{
    //    try
    //    {
    //        using (RijndaelManaged aes = new RijndaelManaged())
    //        {
    //            byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

    //            /* This is for demostrating purposes only. 
    //             * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
    //            byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

    //            using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
    //            {
    //                using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
    //                {
    //                    using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
    //                    {
    //                        using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
    //                        {
    //                            int data;
    //                            while ((data = fsIn.ReadByte()) != -1)
    //                            {
    //                                cs.WriteByte((byte)data);
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message);
    //        // failed to encrypt file
    //    }
    //}

    //private static void DecryptFile(string inputFile, string outputFile, string skey)
    //{
    //    try
    //    {
    //        using (RijndaelManaged aes = new RijndaelManaged())
    //        {
    //            byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

    //            /* This is for demostrating purposes only. 
    //             * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
    //            byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

    //            using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
    //            {
    //                using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
    //                {
    //                    using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
    //                    {
    //                        using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
    //                        {
    //                            int data;
    //                            while ((data = cs.ReadByte()) != -1)
    //                            {
    //                                fsOut.WriteByte((byte)data);
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message);
    //        // failed to decrypt file
    //    }
    //}



    public static void EncryptFile(string sInputFilename,
       string sOutputFilename,
       string sKey)
    {
        try
        {
            FileStream fsInput = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);

            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create,
               FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            // failed to decrypt file
        }

    }

    public static void DecryptFile(string sInputFilename,
       string sOutputFilename,
       string sKey)
    {
        try
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            // failed to decrypt file
        }

    }

    public static void FileEncrypt(string _FilePath,string _Encrypted)
    {
        // Must be 64 bits, 8 bytes.
        // Distribute this key to the user who will decrypt this file.
        string sESecretKey = string.Empty;
        // Get the Key for the file to Encrypt.
        sESecretKey = "1234567890";

        //// For additional security Pin the key.
        //GCHandle gch = GCHandle.Alloc(sESecretKey, GCHandleType.Pinned);

        //// Encrypt the file.        
        //EncryptFile(_FilePath,
        //   _Directory + @"\Encrypted.txt",
        //   sSecretKey);
        // Encrypt the file.        
        EncryptFile(_FilePath,
           _Encrypted,
           sESecretKey);

        //// Decrypt the file.
        //DecryptFile(_Directory + @"\Encrypted.txt",
        //   _Directory + @"\Decrypted.txt",
        //   sSecretKey);

        // Remove the Key from memory. 
        //ZeroMemory(gch.AddrOfPinnedObject(), sESecretKey.Length * 2);
        //gch.Free();

        //System.Windows.Forms.Application.Restart();
    }

    public static void FileDecrypt(string _Encrypted, string _Decrypted)
    {
        // Must be 64 bits, 8 bytes.
        // Distribute this key to the user who will decrypt this file.
        string sDSecretKey = string.Empty;
        // Get the Key for the file to Encrypt.

        sDSecretKey = "1234567890";

        //// For additional security Pin the key.
        //GCHandle gch = GCHandle.Alloc(sDSecretKey, GCHandleType.Pinned);

        //// Encrypt the file.        
        //EncryptFile(_FilePath,
        //   _Directory + @"\Encrypted.txt",
        //   sSecretKey);

        // Decrypt the file.
        DecryptFile(_Encrypted,
           _Decrypted,
           sDSecretKey);

        //// Remove the Key from memory. 
        //ZeroMemory(gch.AddrOfPinnedObject(), sDSecretKey.Length * 2);
        //gch.Free();

        //System.Windows.Forms.Application.Restart();
    }



    public static string Encrypt(string plainText)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
        var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

        byte[] cipherTextBytes;

        using (var memoryStream = new MemoryStream())
        {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
                cryptoStream.Close();
            }
            memoryStream.Close();
        }
        return Convert.ToBase64String(cipherTextBytes);
    }

    public static string Decrypt(string encryptedText)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
        byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

        var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
        var memoryStream = new MemoryStream(cipherTextBytes);
        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
    }

}
