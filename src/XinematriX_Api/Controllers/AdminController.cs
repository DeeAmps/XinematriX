using Microsoft.AspNetCore.Mvc;
using System.Linq;
using XinematriX.DataAccess.DBModels;
using System;
using XinematriX.Api.Providers;
using XinematriX.Models.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XinematriX.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        public static AdminController Instance = new AdminController();

        public AdminController() { }

        public async Task<bool> VerifyAdmin(string username, string cipherText)
        {
            using (var con = new XinematriXContext())
            {
                var data = await Task.Run(() => con.WebAdminAuth.Where(p => p.WebAdminUsername == username && p.WebAdminPassword == cipherText)
                .Select(p => p.WebAdminAuthId).ToList());
                if (data.Count != 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> ValidToken(string tkValue)
        {
            if (tkValue == null || string.IsNullOrEmpty(tkValue))
            {
                return false;
            }
            var tk = tkValue.Split(":");
            var tokenCode = tk[0];
            var encryptedUsername = tk[1];
            var decryptedUsername = TokenHelper.Instance.Decrypt(encryptedUsername);
            using (var con = new XinematriXContext())
            {
                var auth = await Task.Run(() => con.WebAdminAuth.Where
                    (p => p.WebAdminToken == tokenCode 
                    && p.WebAdminUsername == decryptedUsername 
                    && p.WebAdminTokenExpiryDate > DateTime.Now).ToList());
                if (auth.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public async Task<bool> ChangePassword(ProfilePassword pass, string token)
        {
            try
            {
                var tk = token.Split(":");
                var encryptedUsername = tk[1];
                var decryptedUsername = TokenHelper.Instance.Decrypt(encryptedUsername);
                var encryptedNewPassword = TokenHelper.Instance.Encrypt(pass.newpassword);
                using (var con = new XinematriXContext())
                {
                    var user = await Task.Run(() => con.WebAdminAuth.Where(p => p.WebAdminUsername == decryptedUsername).FirstOrDefault());
                    user.WebAdminPassword = encryptedNewPassword;
                    await con.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
            
        }

        public async Task<bool> VerifyPassword(string oldpassword, string token)
        {
            var tk = token.Split(":");
            var encryptedUsername = tk[1];
            var decryptedUsername = TokenHelper.Instance.Decrypt(encryptedUsername);
            var encryptedPassowrd = TokenHelper.Instance.Encrypt(oldpassword);

            using (var con = new XinematriXContext())
            {
                var user = await Task.Run(() => con.WebAdminAuth.Where(p => p.WebAdminUsername == decryptedUsername && p.WebAdminPassword == encryptedPassowrd).FirstOrDefault());
                if (user == null)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<string> CreateWebAccountToken(string username, string code)
        {
            using (var con = new XinematriXContext())
            {
                var data = await Task.Run(() => con.WebAdminAuth.Where(p => p.WebAdminUsername == username).FirstOrDefault());
                data.WebAdminToken = code;
                data.WebAdminTokenGeneratedDate = DateTime.Now;
                data.WebAdminTokenExpiryDate = DateTime.Now.AddHours(1);

                await con.SaveChangesAsync();

                return await Task.Run(() => con.WebAdminAuth.Where(p => p.WebAdminUsername == username
                && p.WebAdminTokenGeneratedDate == data.WebAdminTokenGeneratedDate
                && p.WebAdminTokenExpiryDate == data.WebAdminTokenExpiryDate).FirstOrDefault().WebAdminToken);

            }
        }

        public async void ExpireToken(string tkValue)
        {
            var tk = tkValue.Split(":");
            var tokenCode = tk[0];
            using (var con = new XinematriXContext())
            {
                var auth = await Task.Run(() => con.WebAdminAuth.Where(p => p.WebAdminToken == tokenCode).FirstOrDefault());
                auth.WebAdminTokenExpiryDate = DateTime.Now.AddMinutes(-5);
                await con.SaveChangesAsync();
            }
        }
    }
}
