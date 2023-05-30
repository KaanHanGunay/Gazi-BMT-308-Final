# Web Programlama (BMT-308) Final Ödevi

<p>Gazi Üniversitesi Bilgisayar Mühendisliği Bölümü derslerinden Web Programlama (BMT-308) dersinin final ödevi olarak hazırlanmıştır.</p>

<p>Veritabanı oluşturmak ve kullanmak için;</p>

```sh
docker run --name postgres -e POSTGRES_USER=khg -e POSTGRES_PASSWORD=khg -p 5432:5432 -d postgres
```

<p>Postgresql Driver'ını projeye eklemek için;</p>

```sh
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

