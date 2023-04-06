using System;
using System.Data;
using System.Data.SqlClient;

namespace Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password :");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan :");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database:");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = MSI\\BAGUSAHLUL; " + "initial catalog = {0}; " + "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Ubah Data");
                                        Console.WriteLine("4. Hapus Data");
                                        Console.WriteLine("5. Cari Data");
                                        Console.WriteLine("6. Keluar");
                                        Console.Write("\nEnter your choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA PASIEN\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA PASIEN\n");
                                                    Console.WriteLine("Masukkan ID :");
                                                    string ID = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Pasien :");
                                                    string NmaPasien = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Pasien :");
                                                    string Almt = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Tanggal Datang :");
                                                    string TglDtg = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon :");
                                                    string notlpn = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(ID, NmaPasien, Almt, TglDtg, notlpn, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n ");
                    Console.ResetColor();
                }
            }
            public void baca(SqlConnection con)
            {
                SqlCommand cmd = new SqlCommand("Select * From dbo.Tb_Pasien", con);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        Console.WriteLine(r.GetValue(i));
                    }
                    Console.WriteLine();
                }
                r.Close();
            }
            public void insert(string ID, string NmaPasien, string Almt, string TglDtg, string notlpn, SqlConnection con)
            {

                string str = "";
                str = "insert into dbo.Tb_Pasien (ID,NamaPasien,AlamatPasien,TglDtg,PhonePasien)" + " values(@ID,@nma,@alamat,@TglDtg,@Phn)";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("ID", ID));
                cmd.Parameters.Add(new SqlParameter("Nma", NmaPasien));
                cmd.Parameters.Add(new SqlParameter("Alamat", Almt));
                cmd.Parameters.Add(new SqlParameter("TglDtg", TglDtg));
                cmd.Parameters.Add(new SqlParameter("Phn", notlpn));
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data Berhasil Ditambahkan");
            }
        }
    }
}