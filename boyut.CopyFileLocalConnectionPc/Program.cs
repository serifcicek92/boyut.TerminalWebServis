using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.IO;

namespace boyut.CopyFileLocalConnectionPc
{
    class Program
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll")]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr handle);


        #region Private Variables
       private static IntPtr _TokenHandle = new IntPtr(0);
       private static WindowsImpersonationContext _WindowsImpersonationContext;
       private static WindowsImpersonationContext impersonatedUser;
       private static WindowsIdentity newId;
        #endregion


        //[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Main(string[] args)
        {
            Console.WriteLine("BOYUT DOSYA KOPYALAMA.....");
            Console.WriteLine("Örnek Hedef Klasör  \\\\10.0.0.22\\paylasim\\dosyaadi.txt");
            Console.WriteLine("Örnek Kaynak Klasör  c:\\dosyaadi.txt");
            Console.Write("Serber Adresi Giriniz : ");
            String serverAdresi = Console.ReadLine();
            Console.Write("paylaşılan hedef klasör adı giriniz : ");
            String hedefKlasor = Console.ReadLine().Replace("/","\\");
            Console.Write("Kullanıcı Adı Giriniz : ");
            String kullaniciAdi = Console.ReadLine();
            Console.Write("Şifre Giriniz : ");
            String sifre = Console.ReadLine();
            Console.Write("Kopyalanacak Dosya Yolu Giriniz");
            String kaynak = Console.ReadLine().Replace("/","\\");
            
            //Impersonate._userName = "um1";
            //Impersonate._password = "123456";
            //Impersonate._domain = "192.168.3.11";
            //Impersonate.CopyFile(@"\\192.168.3.11\c$\net\sil\test.txt", @"c:\TEMP\test.txt");

            try
            {
                const int LOGON32_PROVIDER_DEFAULT = 0;
                const int LOGON32_LOGON_INTERACTIVE = 2;



                _TokenHandle = IntPtr.Zero;


                bool returnValue = LogonUser(kullaniciAdi, serverAdresi, sifre, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref _TokenHandle);
                //bool returnValue = LogonUser(userName, domainName, password, 2, 0, ref tokenHandle);
                if (returnValue)
                {
                    //newId = new WindowsIdentity(_TokenHandle);
                    //impersonatedUser = newId.Impersonate();
                    using(WindowsImpersonationContext giris = new WindowsIdentity(_TokenHandle).Impersonate())
                    {
                        try
                        {
                            //string[] allImgs = Directory.GetFiles(@"\\192.168.3.11\net");
                            File.Copy(@hedefKlasor, kaynak, true);
                            
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            throw;
                        }
                        finally
                        {
                            giris.Undo();
                            CloseHandle(_TokenHandle);

                        }
                    }
                    
                    

                }
                else
                {
                    //do some error handling
                    Console.WriteLine("Login Başarısız");
                }

                //Undo impersonation
                if (impersonatedUser != null)
                {
                    impersonatedUser.Undo();
                }
                if (_TokenHandle != IntPtr.Zero)
                {
                    CloseHandle(_TokenHandle);
                }
                Console.ReadLine();

            }
            catch (Exception)
            {

                throw;
            }
            

        }


        //[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        //public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
        //int dwLogonType, int dwLogonProvider, out SafeTokenHandle phToken);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        //public extern static bool CloseHandle(IntPtr handle);


        //// Test harness.
        //// If you incorporate this code into a DLL, be sure to demand FullTrust.
        //[PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        //public static void Main(string[] args)
        //{
        //    Console.Write("Lütfen Server Bağlantısını Giriniz : ");
        //    String serverIp = Console.ReadLine();






        //    //using (FileStream dest = File.OpenWrite(localDestinationFilename))
        //    //using (copy = new ImpersonatedFileCopy(domain, user, pass))
        //    //{
        //    //    success = copy.GetFile(remoteSourceFilename, dest, true);
        //    //}
        //}


        //public static void test()
        //{
        //    SafeTokenHandle safeTokenHandle;
        //    try
        //    {
        //        string userName, domainName;
        //        // Get the user token for the specified user, domain, and password using the
        //        // unmanaged LogonUser method.
        //        // The local machine name can be used for the domain name to impersonate a user on this machine.
        //        Console.Write("Enter the name of the domain on which to log on: ");
        //        domainName = Console.ReadLine();

        //        Console.Write("Enter the login of a user on {0} that you wish to impersonate: ", domainName);
        //        userName = Console.ReadLine();

        //        Console.Write("Enter the password for {0}: ", userName);

        //        const int LOGON32_PROVIDER_DEFAULT = 0;
        //        //This parameter causes LogonUser to create a primary token.
        //        const int LOGON32_LOGON_INTERACTIVE = 2;

        //        // Call LogonUser to obtain a handle to an access token.
        //        bool returnValue = LogonUser(userName, domainName, Console.ReadLine(),
        //            LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
        //            out safeTokenHandle);

        //        Console.WriteLine("LogonUser called.");

        //        if (false == returnValue)
        //        {
        //            int ret = Marshal.GetLastWin32Error();
        //            Console.WriteLine("LogonUser failed with error code : {0}", ret);
        //            throw new System.ComponentModel.Win32Exception(ret);
        //        }
        //        using (safeTokenHandle)
        //        {
        //            Console.WriteLine("Did LogonUser Succeed? " + (returnValue ? "Yes" : "No"));
        //            Console.WriteLine("Value of Windows NT token: " + safeTokenHandle);

        //            // Check the identity.
        //            Console.WriteLine("Before impersonation: "
        //                + WindowsIdentity.GetCurrent().Name);
        //            // Use the token handle returned by LogonUser.
        //            using (WindowsIdentity newId = new WindowsIdentity(safeTokenHandle.DangerousGetHandle()))
        //            {
        //                using (WindowsImpersonationContext impersonatedUser = newId.Impersonate())
        //                {

        //                    // Check the identity.
        //                    Console.WriteLine("After impersonation: "
        //                        + WindowsIdentity.GetCurrent().Name);
        //                }
        //            }
        //            // Releasing the context object stops the impersonation
        //            // Check the identity.
        //            Console.WriteLine("After closing the context: " + WindowsIdentity.GetCurrent().Name);
        //        }

        //        File.Copy(@"\\192.168.3.9\c:\TEMP\bfkiller.log", @"c:\net");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception occurred. " + ex.Message);
        //    }
        //    Console.ReadLine();
        //}

    }



    public class Impersonate
    {
        public static string _userName;
        public static string _domain;
        public static string _password;

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr token);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(string username, string domain, string password, int logonType, int logonProvider, ref IntPtr token);


        /// <summary>
        /// Copies a source file to the destination path using win32.dll and winodws impersonation, 
        /// create the destination directory if it doesnt exists
        /// </summary>
        /// <param name="sourcePath">complete source file path including the filename</param>
        /// <param name="destPath">complete destination file path including the filename</param>
        /// <returns></returns>
        public static bool CopyFile(string sourcePath, string destPath)
        {
            bool copyStatus = true;
            IntPtr token = IntPtr.Zero;
            bool isValid = LogonUser(_userName,
                                    _domain,
                                    _password,
                                    (int)LogonType.Interactive,
                                    (int)LogonProvider.Default,
                                    ref token);
            if (!isValid)
            {
                copyStatus = false;
                int errorCode = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(errorCode);
            }

            using (WindowsImpersonationContext UserContext = WindowsIdentity.Impersonate(token))
            {
                CloseHandle(token);
                var path = Path.GetDirectoryName(destPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                }
                try
                {
                    File.Copy(sourcePath, destPath, true);
                }
                catch (Exception ex)
                {
                    copyStatus = false;
                    throw;
                }


            }

            return copyStatus;
        }



        public static bool CopyFileByteArray(byte[] sourceFileByteArray, string destPath)
        {
            bool copyStatus = true;
            IntPtr token = IntPtr.Zero;
            bool isValid = LogonUser(_userName,
                                    _domain,
                                    _password,
                                    (int)LogonType.Interactive,
                                    (int)LogonProvider.Default,
                                    ref token);
            if (!isValid)
            {
                copyStatus = false;
                int errorCode = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(errorCode);
            }

            using (WindowsImpersonationContext UserContext = WindowsIdentity.Impersonate(token))
            {
                CloseHandle(token);
                var path = Path.GetDirectoryName(destPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                }
                try
                {
                    File.WriteAllBytes(destPath, sourceFileByteArray);
                }
                catch (Exception ex)
                {
                    copyStatus = false;
                    throw;
                }


            }

            return copyStatus;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        public static byte[] FileToByteArray(string sourcePath)
        {
            byte[] fileToByte;
            IntPtr token = IntPtr.Zero;
            bool isValid = LogonUser(_userName,
                                    _domain,
                                    _password,
                                    (int)LogonType.Interactive,
                                    (int)LogonProvider.Default,
                                    ref token);
            if (!isValid)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(errorCode);
            }

            using (WindowsImpersonationContext UserContext = WindowsIdentity.Impersonate(token))
            {
                CloseHandle(token);
                var path = Path.GetDirectoryName(sourcePath);
                if (!Directory.Exists(path))
                {
                    throw new System.IO.FileNotFoundException();
                }
                try
                {
                    fileToByte = File.ReadAllBytes(sourcePath);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return fileToByte;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        public static bool DeleteFiles(string filePath, string partName, string fileType)
        {
            bool isDeleted = false;
            IntPtr token = IntPtr.Zero;
            bool isValid = LogonUser(_userName,
                                    _domain,
                                    _password,
                                    (int)LogonType.Interactive,
                                    (int)LogonProvider.Default,
                                    ref token);
            if (!isValid)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(errorCode);
            }

            using (WindowsImpersonationContext UserContext = WindowsIdentity.Impersonate(token))
            {
                CloseHandle(token);
                var path = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(path))
                {
                    throw new System.IO.FileNotFoundException();
                }
                try
                {
                    var supportFiles = Directory.GetFiles(path)
                    .Where(
                    x => x.ToLower().Contains(partName.ToLower()) && x.ToLower().Contains(fileType.ToLower())
                    ).ToList();

                    if (supportFiles.Count > 0)
                    {
                        foreach (string supportFile in supportFiles)
                        {
                            File.Delete(supportFile);
                        }

                    }


                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return isDeleted;
        }

        /// <summary>
        /// Check the given directory exists
        /// If the directory doesnt exist, then it will be created
        /// </summary>
        /// <param name="destPath"></param>
        /// <returns></returns>
        public static bool CheckOrCreateDir(string destPath)
        {
            bool status = true;
            IntPtr token = IntPtr.Zero;
            bool isValid = LogonUser(_userName,
                                    _domain,
                                    _password,
                                    (int)LogonType.Interactive,
                                    (int)LogonProvider.Default,
                                    ref token);
            if (!isValid)
            {
                status = false;
                int errorCode = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(errorCode);
            }

            using (WindowsImpersonationContext UserContext = WindowsIdentity.Impersonate(token))
            {
                CloseHandle(token);

                if (!Directory.Exists(destPath))
                {
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        status = false;
                        throw;
                    }
                }
            }
            return status;
        }

        enum LogonType
        {
            Interactive = 2,
            Network = 3,
            Batch = 4,
            Service = 5,
            Unlock = 7,
            NetworkClearText = 8,
            NewCredentials = 9
        }

        enum LogonProvider
        {
            Default = 0,
            WinNT35 = 1,
            WinNT40 = 2,
            WinNT50 = 3
        }
    }
}





//public sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
//{
//    private SafeTokenHandle()
//        : base(true)
//    {
//    }

//    [DllImport("kernel32.dll")]
//    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
//    [SuppressUnmanagedCodeSecurity]
//    [return: MarshalAs(UnmanagedType.Bool)]
//    private static extern bool CloseHandle(IntPtr handle);

//    protected override bool ReleaseHandle()
//    {
//        return CloseHandle(handle);
//    }
//}