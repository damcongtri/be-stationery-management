namespace stationeryManagement.Service.Utils;

public class FIleUtils
{
    /// <summary>
    /// Upload image to wwwroot/image
    /// </summary>
    /// <param name="folder">Tên folder sau wwwroot/image </param>
    /// <param name="fileUpload">File</param>
    /// <returns></returns>
    public static async Task<string?> AddFile(string? folder,IFormFile fileUpload)
    {
        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(fileUpload.FileName);
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/"+folder);
        var path = Path.Combine(directoryPath, fileName);

        // Kiểm tra xem thư mục đã tồn tại chưa, nếu không thì tạo mới
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        try
        {
            // Kiểm tra xem tệp tin đã tồn tại chưa
            if (File.Exists(path))
            {
                // Nếu đã tồn tại, bạn có thể xử lý nó theo ý muốn, ví dụ:
                // Thêm một đoạn mã để đổi tên hoặc xử lý tên tệp tin mới tại đây.
                // Ví dụ:
                fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(fileUpload.FileName);
                path = Path.Combine(directoryPath, fileName);
            }

            // Tạo tệp tin và ghi dữ liệu từ fileUpload vào đó
            using (var stream = File.Create(path))
            {
                await fileUpload.CopyToAsync(stream);
            }

            // Áp dụng đường dẫn đúng cho Image
            return $"/images/{folder}/" + Path.GetFileName(path);
        }
        catch (Exception ex)
        {
            // Xử lý exception, ví dụ: in ra console hoặc ghi vào log
            Console.WriteLine($"Error: {ex.Message}");
        }

        return null;
    }

    /// <summary>
    /// Remove File
    /// </summary>
    /// <param name="fileName">Path path file after wwwroot</param>
    public static void RemoveFile(string fileName)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/"+fileName);
        var path = Path.Combine(directoryPath, fileName);
        try
        {
            // Kiểm tra xem tệp tin đã tồn tại chưaư
            if (File.Exists(directoryPath) && !fileName.Contains("default"))
            {
                File.Delete(directoryPath);
            }
        }
        catch (Exception ex)
        {
            // Xử lý exception, ví dụ: in ra console hoặc ghi vào log
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}