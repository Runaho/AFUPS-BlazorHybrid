using System;
using System.IO;
using System.IO.Compression;
using AFUPS.Data;

namespace AFUPS.SharedServices
{
    public static class Archiver
    {
        public static byte[] GenerateArchive(List<FileConvertingProcess> ConvertedFiles)
        {
            byte[] msArchive;
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    using (ZipArchive archive = new ZipArchive(ms, ZipArchiveMode.Update))
                    {
                        foreach (var file in ConvertedFiles)
                        {
                            ZipArchiveEntry orderEntry = archive.CreateEntry(file.FileName, CompressionLevel.SmallestSize); //create a file with this name
                            using (BinaryWriter writer = new BinaryWriter(orderEntry.Open()))
                            {
                                writer.Write(file.Bytes);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    msArchive = ms.ToArray();
                }
                return msArchive;
            }
        }

        public static List<FileModel> ReadArchive(ArchiveFile archiveFile)
        {
            Stream ms = new MemoryStream(archiveFile.ArchiveBytes); // The original data

            ZipArchive _archive = new ZipArchive(ms);
            var files = new List<FileModel>();
            try
            {
                foreach (ZipArchiveEntry entry in _archive.Entries)
                {
                    files.Add(new FileModel { Name = entry.Name, Length = entry.Length });
                }
            }
            catch (Exception ex)
            {

            }

            return files;
        }
    }
}




