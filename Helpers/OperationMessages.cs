
namespace WeGout.Helpers
{
    public static class OperationMessages{
        public const string GeneralError = "Hata nedeniyle işleminiz gerçekleştirilemedi";

        public const string DbError = "Veritabanı hatası nedeni ile işlem gerçekleştirilemedi";

        public const string DbItemNotFound = "Güncellenecek kayıt bilgisi bulunamadığından işlem gerçekleştirilemedi";

        public const string NoChanges = "Kayıtta değişiklik yapılmadığı için kayıt edilmedi.";

        public const string Success = "İşleminiz başarıyla gerçekleştirildi";

        public const string ModelStateNotValid = "Lütfen girilen bilgileri kontrol edip tekrar deneyiniz";

        public const string DuplicateRecord = "Bu kayıt daha önceden eklendiğinden tekrar eklenemez.";
    }
}