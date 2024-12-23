using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VeriErisimKatmani
{
    public class VeriModel
    {
        SqlConnection baglanti;
        SqlCommand komut;

        public VeriModel()
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=KoMat_Kutuphane; Integrated Security=True");
            komut = baglanti.CreateCommand();
        }

        #region Dil Metotları

        public List<Dil> DilListele()
        {
            List<Dil> diller = new List<Dil>();

            try
            {
                komut.CommandText = "SELECT ID,Dil FROM Diller";
                komut.Parameters.Clear();
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Dil model = new Dil();
                    model.ID = okuyucu.GetInt32(0);
                    model.Isim = okuyucu.GetString(1);
                    diller.Add(model);
                }
                return diller;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool DilEkle(Dil model)
        {
            try
            {
                komut.CommandText = "INSERT INTO Diller(Dil) VALUES(@isim)";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@isim", model.Isim);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public Dil DilGetir(int id)
        {
            try
            {
                komut.CommandText = "SELECT ID, Dil FROM Diller WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                Dil model = new Dil();
                while (okuyucu.Read())
                {
                    model.ID = okuyucu.GetInt32(0);
                    model.Isim = okuyucu.GetString(1);
                }
                return model;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }


        public bool DilGuncelle(Dil model)
        {
            try
            {
                komut.CommandText = "UPDATE Diller SET Dil=@d WHERE ID = @id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", model.ID);
                komut.Parameters.AddWithValue("@d", model.Isim);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public void DilSil(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM Diller WHERE ID = @id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }


        #endregion

        #region Yazar Metotları

        public List<Yazar> YazarListele()
        {
            try
            {
                List<Yazar> yazarlar = new List<Yazar>();
                komut.CommandText = "SELECT ID, Isim, SoyIsim FROM Yazarlar";
                komut.Parameters.Clear();
                baglanti.Open();

                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Yazar model = new Yazar();
                    model.ID = okuyucu.GetInt32(0);
                    model.Isim = okuyucu.GetString(1);
                    if (!okuyucu.IsDBNull(2))
                    {
                        model.SoyIsim = okuyucu.GetString(2);
                    }
                    yazarlar.Add(model);
                }
                return yazarlar;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool YazarEkle(Yazar model)
        {
            try
            {
                komut.CommandText = "INSERT INTO Yazarlar(Isim, SoyIsim) VALUES(@isim, @soyisim)";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@isim", model.Isim);
                komut.Parameters.AddWithValue("@soyisim", model.SoyIsim);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public Yazar YazarGetir(int id)
        {
            try
            {
                Yazar model = new Yazar();
                komut.CommandText = "SELECT ID, Isim, SoyIsim  FROM Yazarlar WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    model.ID = okuyucu.GetInt32(0);
                    model.Isim = okuyucu.GetString(1);
                    if (!okuyucu.IsDBNull(2))
                    {
                        model.SoyIsim = okuyucu.GetString(2);
                    }
                }
                return model;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close(); 
            }
        }

        public bool YazarGuncelle(Yazar model)
        {
            try
            {
                komut.CommandText = "UPDATE Yazarlar SET Isim=@i, SoyIsim=@s WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("id", model.ID);
                komut.Parameters.AddWithValue("i", model.Isim);
                komut.Parameters.AddWithValue("s", model.SoyIsim);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void YazarSil(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM Yazarlar WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        public int YazarKitapSayi(int id)
        {
            try

            {
                komut.CommandText = "SELECT COUNT(*) FROM KitapYazarlar WHERE YazarID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("id", id);
                baglanti.Open();
                int sayi = Convert.ToInt32(komut.ExecuteScalar());
                return sayi;
            }
            finally
            {
                baglanti.Close();
            }
        }

        #endregion

        #region Tür Metotları

        public List<Tur> TurListele()
        {
            List<Tur> turler = new List<Tur>();

            try
            {
                komut.CommandText = "SELECT ID, Isim FROM Turler";
                komut.Parameters.Clear();
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Tur model = new Tur();
                    model.ID = okuyucu.GetInt32(0);
                    model.Isim = okuyucu.GetString(1);
                    turler.Add(model);
                }
                return turler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool TurEkle(Tur model)
        {
            try
            {
                komut.CommandText = "INSERT INTO Turler(Isim) VALUES(@isim)";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@isim", model.Isim);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public Tur TurGetir(int id)
        {
            try
            {
                komut.CommandText = "SELECT ID, Isim FROM Turler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                Tur model = new Tur();
                while (okuyucu.Read())
                {
                    model.ID = okuyucu.GetInt32(0);
                    model.Isim = okuyucu.GetString(1);
                }
                return model;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool TurGuncelle(Tur model)
        {
            try
            {
                komut.CommandText = "UPDATE Turler SET Isim=@t WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", model.ID);
                komut.Parameters.AddWithValue("@t", model.Isim);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void TurSil(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM Turler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        #endregion

        #region YayinEvi Metotları

        public List<YayinEvleri> YayinEviListele()
        {
            List<YayinEvleri> yayinevleri = new List<YayinEvleri>();

            try
            {
                komut.CommandText = "SELECT ID, Isim FROM YayinEvleri";
                komut.Parameters.Clear();
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    YayinEvleri model = new YayinEvleri();
                    model.ID = okuyucu.GetInt32(0);
                    model.Isim = okuyucu.GetString(1);
                    yayinevleri.Add(model);
                }
                return yayinevleri;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool YayinEviEkle(YayinEvleri model)
        {
            try
            {
                komut.CommandText = "INSERT INTO YayinEvleri(Isim) VALUES(@isim)";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@isim", model.Isim);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public YayinEvleri YayinEviGetir(int id)
        {
            try
            {
                komut.CommandText = "SELECT ID, Isim FROM YayinEvleri WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                YayinEvleri model = new YayinEvleri();
                while (okuyucu.Read())
                {
                    model.ID = okuyucu.GetInt32(0);
                    model.Isim = okuyucu.GetString(1);
                }
                return model;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool YayinEviGuncelle(YayinEvleri model)
        {
            try
            {
                komut.CommandText = "UPDATE YayinEvleri SET Isim=@y WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", model.ID);
                komut.Parameters.AddWithValue("@y", model.Isim);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void YayinEviSil(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM YayinEvleri WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        #endregion


    }
}
