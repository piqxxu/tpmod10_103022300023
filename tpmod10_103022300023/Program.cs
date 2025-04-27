using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

// Endpoint GET /api/mahasiswa
app.MapGet("/api/mahasiswa", () =>
{
    // Mengembalikan list mahasiswa dari variabel statis di bawah
    return Results.Ok(MahasiswaData.dataMahasiswa);
});

// Endpoint GET /api/mahasiswa/{index}
app.MapGet("/api/mahasiswa/{index}", (int index) =>
{
    var data = MahasiswaData.dataMahasiswa; // Ambil list
    if (index < 0 || index >= data.Count)
    {
        return Results.NotFound($"Mahasiswa dengan index {index} tidak ditemukan.");
    }
    return Results.Ok(data[index]);
});

// Endpoint POST /api/mahasiswa
app.MapPost("/api/mahasiswa", ([FromBody] Mahasiswa inputMahasiswa) =>
{
    var data = MahasiswaData.dataMahasiswa; // Ambil list
    data.Add(inputMahasiswa); // Tambahkan
    return Results.Created($"/api/mahasiswa/{data.Count - 1}", inputMahasiswa);
});

// Endpoint DELETE /api/mahasiswa/{index}
app.MapDelete("/api/mahasiswa/{index}", (int index) =>
{
    var data = MahasiswaData.dataMahasiswa; // Ambil list
    if (index < 0 || index >= data.Count)
    {
        return Results.NotFound($"Mahasiswa dengan index {index} tidak ditemukan.");
    }
    data.RemoveAt(index); // Hapus
    return Results.Ok($"Mahasiswa dengan index {index} berhasil dihapus.");
});

// Endpoint default
app.MapGet("/", () => "Selamat datang di API Mahasiswa!");

// Jalankan aplikasi
app.Run();

public class Mahasiswa
{
    public string Nama { get; set; }
    public string Nim { get; set; }

    // Konstruktor opsional, bisa dihapus jika tidak dipakai langsung
    public Mahasiswa(string nama, string nim)
    {
        Nama = nama;
        Nim = nim;
    }
    // Diperlukan konstruktor tanpa parameter untuk deserialisasi JSON dari POST request
    public Mahasiswa() { }
}

// Kelas statis untuk menyimpan data (agar lebih rapi)
public static class MahasiswaData
{
    public static List<Mahasiswa> dataMahasiswa { get; private set; } = new List<Mahasiswa>
    {
        // Data dari JSON yang kamu berikan:
        new Mahasiswa("Azwa Radya", "103022300023"),
        new Mahasiswa("Indra Yuda Adi Saputra", "103022300017"),
        new Mahasiswa("April Hardinata", "103022300027"),
        new Mahasiswa("Andreas Christian Firga", "103022300003"),
        new Mahasiswa("Emillio Syailendra Wijaya", "103022330124")
    };
}