package pages.commonPages;

import com.microsoft.playwright.Download;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Arrays;
import java.util.Comparator;
import java.util.stream.Stream;

public class FileUtils {

    /**
     * Bu metot, kullanıcının indirme klasöründe (~/Downloads) dosya adları verilen öneklerden biriyle başlayan
     * tüm dosyaları tarar ve son değiştirilme zamanına göre en güncel olan dosyayı döndürür.
     * @param filePrefixes İlgili dosyaların adlarının başlangıç önekleri. Örneğin, "ProductUploadTemplate".
     * @return En son indirilen dosyanın {@link Path} nesnesi.
     * @throws IOException Eğer dosya bulunamazsa veya klasöre erişim sırasında hata oluşursa hata fırlatılır.
     */
    public static Path getLatestDownloadedFile(String... filePrefixes) throws IOException {
        String downloadPath = System.getProperty("user.home") + "/Downloads";
        try (Stream<Path> files = Files.list(Paths.get(downloadPath))) {
            return files
                    .filter(f -> {
                        String name = f.getFileName().toString();
                        return Arrays.stream(filePrefixes).anyMatch(name::startsWith);
                    })
                    .filter(f -> f.toFile().isFile())
                    .max(Comparator.comparingLong(f -> f.toFile().lastModified()))
                    .orElseThrow(() -> new IOException("Dosya bulunamadı"));
        }
    }

    /**
     * Playwright kullanılarak indirilen Excel dosyasının başarıyla indirilip indirilmediğini doğrular.
     * @param download Playwright tarafından indirilen dosyayı temsil eden {@link Download} nesnesi.
     * @return {@code true} eğer dosya mevcutsa ve boyutu sıfırdan büyükse; aksi takdirde {@code false}.
     * Playwright, indirme işlemi sırasında dosyayı varsayılan olarak kendi geçici dizinine kaydeder.
     * Bu nedenle Downloads klasörü yerine genellikle sistem geçici dizinine (Temp, /tmp, vb.) kaydedilir.
     */
    public static boolean verifyExcelDownloadWithPlaywright(Download download) {
        try {
            Path filePath = download.path();
            return Files.exists(filePath) && Files.size(filePath) > 0;
        } catch (IOException e) {
            return false;
        }
    }

}
