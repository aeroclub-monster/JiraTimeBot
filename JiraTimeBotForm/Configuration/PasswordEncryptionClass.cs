﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace JiraTimeBotForm.Configuration
{
    public class PasswordEncryptionClass
    {
        private byte[] GetEntropy(string entropyString)
        {

            MD5 md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(entropyString));
        }

        public string Encrypt(string username, string password, string address)
        {
            byte[] entropy = GetEntropy(address + username);
            byte[] pass = Encoding.UTF8.GetBytes(password);
            byte[] crypted = ProtectedData.Protect(pass, entropy, DataProtectionScope.LocalMachine);
            var cryptedBase64 = Convert.ToBase64String(crypted);

            return cryptedBase64;
        }

        public string Decrypt(string username, string password, string address)
        {
            try
            {
                var pass = Convert.FromBase64String(password);
                byte[] entropy = GetEntropy(address + username);
                pass = ProtectedData.Unprotect(pass, entropy, DataProtectionScope.LocalMachine);
                return Encoding.UTF8.GetString(pass);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}