using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.Globalization;

namespace Plus54PortfolioRedesign2014.Common
{

    /// <summary>
    /// Provides an interface to encrypt and decrypt any text string
    /// </summary>
    /// <remarks></remarks>
    public class Encryptor
    {

        //Basic encryption settings
        private string basicEncryptionKey = ConfigurationManager.AppSettings["Encryption.Basic.Pass.Phrase"];
        private byte[] key = {
		
	};
        private byte[] IV = {
		0x12,
		0x34,
		0x56,
		0x78,
		0x90,
		0xab,
		0xcd,
		0xef

	};
        //Advanced encryption settings
        private string passPhrase = ConfigurationManager.AppSettings["Encryption.Pass.Phrase"];
        private string saltValue = ConfigurationManager.AppSettings["Encryption.Salt.Value"];
        private string hashAlgorithm = ConfigurationManager.AppSettings["Encryption.Hash.Algorithm"];
        private int passwordIterations = int.Parse(ConfigurationManager.AppSettings["Encryption.Password.Iterations"], CultureInfo.InvariantCulture);
        private string initVector = ConfigurationManager.AppSettings["Encryption.Init.Vector"];

        private int keySize = int.Parse(ConfigurationManager.AppSettings["Encryption.Key.Size"], CultureInfo.InvariantCulture);
        /// <summary>
        /// Encrypts a string using MD5
        /// </summary>
        /// <param name="textEncrypt">Text to be encrypted</param>
        /// <returns>Encrypted text</returns>
        /// <remarks></remarks>
        public static string EncryptMD5(string textEncrypt)
        {
            //Create an encoding object to ensure the encoding standard for the source text
            UnicodeEncoding Ue = new UnicodeEncoding();

            //Retrieve a byte array based on the source text
            byte[] ByteSourceText = Ue.GetBytes(textEncrypt);

            //Instantiate an MD5 Provider object
            using (MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider())
            {
                //Compute the hash value from the source
                byte[] ByteHash = Md5.ComputeHash(ByteSourceText);

                //And convert it to String format for return
                return Convert.ToBase64String(ByteHash);
            }
        }

        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm and returns a base64-encoded result
        /// </summary>
        /// <param name="plainText">Plaintext value to be encrypted</param>
        /// <param name="passPhrase">Passphrase from which a pseudo-random password will be derived. The derived password will be used to generate the encryption key</param>
        /// <param name="saltValue">Salt value used along with passphrase to generate password. Salt can be any string. In this example we assume that salt is an ASCII string</param>
        /// <param name="hashAlgorithm">Hash algorithm used to generate password. Values are: "MD5" and "SHA1"</param>
        /// <param name="passwordIterations">Number of iterations used to generate password. One or two iterations should be enough</param>
        /// <param name="initVector">Initialization vector (or IV). This value is required to encrypt the first block of plaintext data</param>
        /// <param name="keySize">Size of encryption key in bits. Allowed values are: 128, 192, and 256</param>
        /// <returns>Encrypted value formatted as a base64-encoded string</returns>
        /// <remarks></remarks>
        private static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = null;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes = null;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = null;
            plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            byte[] keyBytes = null;
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations))
            {
                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                keyBytes = password.GetBytes(keySize / 8);
            }

            // Create uninitialized Rijndael encryption object.
            ICryptoTransform encryptor = null;
            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate encryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            }

            // Define memory stream which will be used to hold encrypted data.
            // Define cryptographic stream (always use Write mode for encryption).
            byte[] cipherTextBytes = null;
            using (MemoryStream memoryStream = new MemoryStream())
            //using ()
            {
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                // Start encrypting.
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memory stream into a byte array.
                cipherTextBytes = memoryStream.ToArray();
            }

            // Convert encrypted data into a base64-encoded string.
            string cipherText = null;
            cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }

        /// <summary>
        /// Decrypts specified ciphertext using Rijndael symmetric key algorithm
        /// </summary>
        /// <param name="cipherText">Base64-formatted ciphertext value</param>
        /// <param name="passPhrase">Passphrase from which a pseudo-random password will be derived. The derived password will be used to generate the encryption key</param>
        /// <param name="saltValue">Salt value used along with passphrase to generate password. Salt can be any string. In this example we assume that salt is an ASCII string</param>
        /// <param name="hashAlgorithm">Hash algorithm used to generate password. Values are: "MD5" and "SHA1"</param>
        /// <param name="passwordIterations">Number of iterations used to generate password. One or two iterations should be enough</param>
        /// <param name="initVector">Initialization vector (or IV). This value is required to encrypt the first block of plaintext data</param>
        /// <param name="keySize">Size of encryption key in bits. Allowed values are: 128, 192, and 256</param>
        /// <returns>Decrypted string value</returns>
        /// <remarks></remarks>
        private static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = null;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes = null;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = null;
            cipherTextBytes = Convert.FromBase64String(cipherText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            byte[] keyBytes = null;
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations))
            {

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                keyBytes = password.GetBytes(keySize / 8);
            }

            // Create uninitialized Rijndael encryption object.
            ICryptoTransform decryptor = null;

            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate decryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            }

            // Define memory stream which will be used to hold encrypted data.
            // Define memory stream which will be used to hold encrypted data.
            byte[] plainTextBytes = null;
            int decryptedByteCount = 0;
            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
            {
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold ciphertext;
                // plaintext is never longer than ciphertext.
                plainTextBytes = new byte[cipherTextBytes.Length + 1];

                // Start decrypting.
                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            }

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = null;
            plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            // Return decrypted string.
            return plainText;
        }

        /// <summary>
        /// Encrypts a string (strong)
        /// </summary>
        /// <param name="textToEncrypt">Text to encrypt</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string DoEncrypt(string textToEncrypt)
        {
            string cipherText = Encrypt(textToEncrypt, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);

            return cipherText;
        }

        /// <summary>
        /// Decrypts a string (strong)
        /// </summary>
        /// <param name="textToDecrypt">Text to decrypt</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string DoDecrypt(string textToDecrypt)
        {
            string plainText = Decrypt(textToDecrypt, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);

            return plainText;
        }

        /// <summary>
        /// Encrypts a string (basic)
        /// </summary>
        /// <param name="textToEncrypt">Text to encrypt</param>
        /// <returns>Retunrs the encrypted text</returns>
        /// <remarks></remarks>
        public static string DoEncryptBasic(string textToEncrypt)
        {
            return Encryptor.EncryptString(textToEncrypt, null);
        }

        /// <summary>
        /// Decrypts a string (basic)
        /// </summary>
        /// <param name="textToDecrypt">Text to decrypt</param>
        /// <returns>Retunrs the plain text</returns>
        /// <remarks></remarks>
        public static string DoDecryptBasic(string textToDecrypt)
        {
            return Encryptor.DecryptString(textToDecrypt, null);
        }

        /// <summary>
        /// Replaces special characters
        /// </summary>
        /// <param name="originalText"></param>
        /// <param name="forEncryption"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string ReplaceSpecialCharacters(string originalText, bool forEncryption)
        {
            if (forEncryption)
            {
                return originalText.Replace("+", "*").Replace("/", "$").Replace("=", "@");
            }
            else
            {
                return originalText.Replace("*", "+").Replace("$", "/").Replace("@", "=");
            }
        }

        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm and returns a base64-encoded result
        /// </summary>
        /// <param name="stringToEncrypt">Plaintext value to be encrypted</param>
        /// <param name="SEncryptionKey">Passphrase from which a pseudo-random password will be derived. The derived password will be used to generate the encryption key</param>
        /// <returns>Encrypted value formatted as a base64-encoded string</returns>
        /// <remarks></remarks>
        private static string EncryptString(string stringToEncrypt, string SEncryptionKey)
        {
            Encryptor enc = new Encryptor();
            if (SEncryptionKey == null)
                SEncryptionKey = enc.basicEncryptionKey;

            try
            {
                enc.key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.PadLeft(8));

                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);

                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(enc.key, enc.IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();

                    return ReplaceSpecialCharacters(Convert.ToBase64String(ms.ToArray()), true);
                }

            }
            catch (NotSupportedException e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Decrypts specified ciphertext using Rijndael symmetric key algorithm
        /// </summary>
        /// <param name="stringToDecrypt">Base64-formatted ciphertext value</param>
        /// <param name="SEncryptionKey">Passphrase from which a pseudo-random password will be derived. The derived password will be used to generate the encryption key</param>
        /// <returns>Decrypted string value</returns>
        /// <remarks></remarks>
        private static string DecryptString(string stringToDecrypt, string sEncryptionKey)
        {
            Encryptor enc = new Encryptor();
            stringToDecrypt = ReplaceSpecialCharacters(stringToDecrypt, false);

            if (sEncryptionKey == null)
                sEncryptionKey = enc.basicEncryptionKey;

            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                enc.key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.PadLeft(8));
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                using (MemoryStream ms = new MemoryStream())
                {
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(enc.key, enc.IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();

                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;

                    return encoding.GetString(ms.ToArray());
                }
            }
            catch (NotSupportedException e)
            {
                return e.Message;
            }
        }
    }
}
