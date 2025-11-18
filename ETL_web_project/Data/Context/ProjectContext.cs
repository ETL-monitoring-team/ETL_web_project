using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Data.Context
{
    public class ProjectContext : DbContext
    {
        // üstteki DbContext biz databaseimizi baglayabilelim diye kalıtım aldık
        //onu sil üstteki using kısmını da sil yine DbContext diye yaz oraya tab dediğinde o using ifadesi kendiliğinden geliyor. Burada bizim eklediğimiz paketlerden birini kullanıyoruz

        //paketleri görmek icin ::  dependencies -> packages orada hepsi var
        // paket ekleme: tools-> nuget package manager -> nuget package solution -> indirme kısmından bizim .net framework ne ise ona uyan sürümü indiriyoruz. sürümümüz 8 olduğu için 8'in son sürümünü indirdim. Ona her zaman dikkat. Zaten yanlıslıkla 10 indirirsen uyarıyor sürümler eşleşmiyor diye

        //Cok önemli bir ince detay info: database tarafında tablo adı çoğul yazılır, mvc tarafında data tekil yazılıyor. Bunu tabi dikkat etmeden kullananlar da cok ama genel bi bilgi olsun buna göre eklersin ask entities sınıfına tablolarımızı


        //MİGRATİON ALMADAN ÖNCE ENTİTYLER EKLENDİKTEN SONRA YAPILACAK KISIM BU

        //   public DbSet< /*bizim entity sınıfımız*/ > /*tablodaki adlandırması*/ { get; set; }
        
        //Bu alttaki dısında onmodelcreating mi bir kısım daha var. Birecok cokacok falan yazdığımız. Chatgptden bi bizim tablolara göre örnek project context bakarsın ona gerek duymus mu diye.
        
        // ekstra alttakini nasıl actım:    override yaz, yanına onconfiguring deyip tab
        //                                      dediğinde bu kendiliğinden oluşuyor. İçine bizim gecenki projede json dosyasında yaptığımız gibi bir sqlservera bağlantı ekledim
        // base. kısmı silinmiyor overrideda kendiliğinden geldi sadece araya bağlantı ekliyoruz
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //kendi server namelerimiz kullanılacak
            optionsBuilder.UseSqlServer("Server=LAPTOP-BCGODK3K;Database=EtlDb;Trusted_Connection=true;TrustServerCertificate=true");

            base.OnConfiguring(optionsBuilder);
        }

        
    }
}
