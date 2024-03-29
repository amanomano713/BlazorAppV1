﻿using System.Security.Cryptography;
using System.Text;

namespace BlazorApp.Encryptor
{
	public class Encryption : IEncryptor
	{

		#region "Estructura y variables privadas"


		internal const string password = "CLAVEENCRIPTACION";
        #endregion

        #region "Funciones de ecriptación y desencriptación"

        /// <summary> 
        /// Encriptacion
        /// </summary> 
        /// <param name="input">String to encrypt</param> 
        /// <returns>Encrypted string</returns> 
#pragma warning disable CS8767 // La nulabilidad de los tipos de referencia del tipo de parámetro no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        public string EnCryption(string? input)
#pragma warning restore CS8767 // La nulabilidad de los tipos de referencia del tipo de parámetro no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        {

			byte[] utfData = System.Text.UTF8Encoding.UTF8.GetBytes(input);
			byte[] saltBytes = Encoding.UTF8.GetBytes(password);
			string encryptedString = null;

			using (AesManaged aes = new AesManaged())
			{
				Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, saltBytes);

				aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
				aes.KeySize = aes.LegalKeySizes[0].MaxSize;
				aes.Key = rfc.GetBytes(aes.KeySize / 8);
				aes.IV = rfc.GetBytes(aes.BlockSize / 8);

				using (ICryptoTransform encryptTransform = aes.CreateEncryptor())
				{
					using (MemoryStream encryptedStream = new MemoryStream())
					{
						using (CryptoStream encryptor = new CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write))
						{
							encryptor.Write(utfData, 0, utfData.Length);
							//encryptor.Flush();
							//encryptor.Close();
							encryptor.FlushFinalBlock();

							byte[] encryptBytes = encryptedStream.ToArray();
							encryptedString = Convert.ToBase64String(encryptBytes);
						}
					}
				}
			}

			return encryptedString;

		}

        /// <summary> 
        /// DesEncriptacion a string 
        /// </summary> 
        /// <param name="input">Input string in base 64 format</param> 
        /// <returns>Decrypted string</returns> 
#pragma warning disable CS8767 // La nulabilidad de los tipos de referencia del tipo de parámetro no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        public string Decryption(string input)
#pragma warning restore CS8767 // La nulabilidad de los tipos de referencia del tipo de parámetro no coincide con el miembro implementado de forma implícita (posiblemente debido a los atributos de nulabilidad).
        {

			byte[] encryptedBytes = Convert.FromBase64String(input);
			byte[] saltBytes = Encoding.UTF8.GetBytes(password);
			string decryptedString = null;

			using (AesManaged Aes = new AesManaged())
			{
				Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, saltBytes);
				Aes.BlockSize = Aes.LegalBlockSizes[0].MaxSize;
				Aes.KeySize = Aes.LegalKeySizes[0].MaxSize;
				Aes.Key = rfc.GetBytes(Aes.KeySize / 8);
				Aes.IV = rfc.GetBytes(Aes.BlockSize / 8);

				using (ICryptoTransform decryptTransform = Aes.CreateDecryptor())
				{
					using (MemoryStream decryptedStream = new MemoryStream())
					{
						CryptoStream decryptor = new CryptoStream(decryptedStream, decryptTransform, CryptoStreamMode.Write);
						decryptor.Write(encryptedBytes, 0, encryptedBytes.Length);
						decryptor.Flush();
						decryptor.Close();

						byte[] decryptBytes = decryptedStream.ToArray();
						decryptedString = UTF8Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
					}
				}
			}

			return decryptedString;

		}


        #endregion

    }
}
