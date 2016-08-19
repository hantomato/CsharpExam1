using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;

namespace CsharpExam1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }


        static byte[] Decompress_old(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
                                  CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        public static void Compress(DirectoryInfo directorySelected, string directoryPath)
        {
            // https://msdn.microsoft.com/ko-kr/library/system.io.compression.gzipstream(v=vs.110).aspx
            foreach (FileInfo fileToCompress in directorySelected.GetFiles())
            {
                using (FileStream originalFileStream = fileToCompress.OpenRead())
                {
                    if ((File.GetAttributes(fileToCompress.FullName) &
                       FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                    {
                        using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                               CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);

                            }
                        }
                        FileInfo info = new FileInfo(directoryPath + "\\" + fileToCompress.Name + ".gz");
                        Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                        fileToCompress.Name, fileToCompress.Length.ToString(), info.Length.ToString());
                    }

                }
            }
        }

        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string directoryPath = @"d:\temp";
            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            Compress(directorySelected, directoryPath);
            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
            {
                Decompress(fileToDecompress);
            }


            ////System.IO.Compression.GZipStream gzipStream = new System.IO.Compression.GZipStream(null )
            ////Stream input = GenerateStreamFromString("Why is it that so many answers that apparently don't work are voted higher than");
            ////GZipStream zip = new GZipStream(input, CompressionMode.Compress);
            ////long len = zip.Length;
            ////GZipStream unzip = new GZipStream(zip, CompressionMode.Decompress);
            ////long len2 = unzip.Length;

            //String stringTemp = "Why is it that so many answers that apparently don't work are voted higher than";

            ////MemoryStream src = new MemoryStream();
            ////GZipStream zip = new GZipStream(src, CompressionMode.Compress);
            ////zip.Write(GetBytes(stringTemp), 0, stringTemp.Length);

            ////MemoryStream desc = new MemoryStream();
            ////GZipStream unzip = new GZipStream(desc, CompressionMode.Decompress);
            ////unzip.Write(src.ToArray(), 0, (int)src.Length);


            //MemoryStream desc = new MemoryStream();

            //compressGZip(desc, GenerateStreamFromString(stringTemp));

            //MemoryStream mmm = decompressGZip(desc);


            //int jj;
            //jj = 33;

        }

        private void compressGZip(Stream desc, Stream src)
        {
            using (var gzip = new GZipStream(desc, CompressionMode.Compress))
            {
                src.CopyTo(gzip);
                gzip.Close();
            }
        }

        private MemoryStream decompressGZip(Stream src)
        {
            var outputStream = new MemoryStream();
            using (var gzip = new GZipStream(src, CompressionMode.Decompress))
            {
                gzip.CopyTo(outputStream);
                gzip.Close();
                outputStream.Position = 0;
            }
            return outputStream;
        }




        private void button2_Click(object sender, EventArgs e)
        {
            // 파일을 읽어서 gzip후 파일로 저장.
            String sourceFileName = "d:\\temp2\\aaa.txt";
            using (FileStream fsSource = File.Open(sourceFileName, FileMode.Open))
            {
                using (FileStream compressedFileStream = File.Create(sourceFileName + ".gz"))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                       CompressionMode.Compress))
                    {
                        fsSource.CopyTo(compressionStream);
                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 파일을 읽어서 gzip후 메모리 스트림에 저장.
            String sourceFileName = "d:\\temp2\\aaa.txt";
            using (FileStream fsSource = File.Open(sourceFileName, FileMode.Open))
            {
                MemoryStream aaa = new MemoryStream();
                //using (FileStream compressedFileStream = File.Create(sourceFileName + ".gz"))
                using (MemoryStream compressedFileStream = new MemoryStream())
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                       CompressionMode.Compress))
                    {
                        fsSource.CopyTo(compressionStream);


                        //compressedFileStream.Position = 0;
                        //var sr = new StreamReader(compressedFileStream);
                        //var myStr = sr.ReadToEnd();
                        //Console.WriteLine(myStr);
                    }
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            String sourceFileName = "d:\\temp2\\aaa.txt.gz";
            using (FileStream fsSource = File.Open(sourceFileName, FileMode.Open))
            {
                using (MemoryStream targetStream = new MemoryStream())
                {
                    using (GZipStream decompressedSourceStream = new GZipStream(fsSource,
                       CompressionMode.Decompress))
                    {
                        decompressedSourceStream.CopyTo(targetStream);


                        targetStream.Position = 0;
                        var sr = new StreamReader(targetStream);
                        var myStr = sr.ReadToEnd();
                        Console.WriteLine(myStr);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //String sourceFileName = "d:\\temp2\\aaa.txt.gz";
            //using (FileStream fsSource = File.Open(sourceFileName, FileMode.Open))
            //{
            //    byte[] fileBytes = new byte[fsSource.Length];
            //    fsSource.Read(fileBytes, 0, fileBytes.Length);

            //    MemoryStream res = convert3(fileBytes);
            //    res.Position = 0;
            //    var sr = new StreamReader(res);
            //    var myStr = sr.ReadToEnd();
            //    Console.WriteLine(myStr);
            //    //byte[] retByte = new byte[1024];
            //    //Decompress2(fileBytes, out retByte);


            //    //using (MemoryStream targetStream = new MemoryStream())
            //    //{
            //    //    using (GZipStream decompressedSourceStream = new GZipStream(fsSource,
            //    //       CompressionMode.Decompress))
            //    //    {
            //    //        decompressedSourceStream.CopyTo(targetStream);


            //    //        targetStream.Position = 0;
            //    //        var sr = new StreamReader(targetStream);
            //    //        var myStr = sr.ReadToEnd();
            //    //        Console.WriteLine(myStr);
            //    //    }
            //    //}
            //}

        }
        private void convert(byte[] source)
        {
            MemoryStream result = new MemoryStream();
            using (GZipStream decompressedSourceStream = new GZipStream(new MemoryStream(source), CompressionMode.Decompress))
            {
                decompressedSourceStream.CopyTo(result);


                result.Position = 0;
                var sr = new StreamReader(result);
                var myStr = sr.ReadToEnd();
                Console.WriteLine(myStr);
            }
        }
        private MemoryStream convert2(byte[] source)
        {
            MemoryStream result = new MemoryStream();
            using (GZipStream decompressedSourceStream = new GZipStream(new MemoryStream(source), CompressionMode.Decompress))
            {
                decompressedSourceStream.CopyTo(result);
                return result;
            }
        }
        //private MemoryStream convert3(byte[] source)
        //{
        //    ICSharpCode.SharpZipLib.GZip.GZipInputStream
        //    MemoryStream result = new MemoryStream();
        //    using (ICSharpCode.SharpZipLib.GZip.GZipInputStream decompressedSourceStream = new ICSharpCode.SharpZipLib.GZip.GZipInputStream(new MemoryStream(source)))
        //    {
        //        decompressedSourceStream.CopyTo(result);
        //        return result;
        //    }
        //}

        private byte[] amb()
        {
            //using (var memoryStream = new MemoryStream())
            //{
            //    using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
            //    {
            //        using (var writer = new StreamWriter(gzipStream))
            //        {
            //            writer.Write("helloworld");
            //        }
            //    }
            //    return memoryStream.ToArray();
            //}

            var memoryStream = new MemoryStream();
            var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress);
            var writer = new StreamWriter(gzipStream);
            {
                writer.Write("helloworld");
            }
            
            return memoryStream.ToArray();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] aaa = amb();
            int jj;
            jj = 33;
        }

        private void DecompressGZipNmj(byte[] source)
        {
            var memoryStream = new MemoryStream(source);
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    using (var writer = new StreamWriter(gzipStream))
                    {
                        //writer.Write("helloworld");
                        
                    }
                }
                
            }
        }

        //public static void Decompress2(byte[] data, out byte[] ret)
        //{
        //    ICSharpCode.SharpZipLib.GZip.GZipInputStream


        //    //using (MemoryStream ms = new MemoryStream(data))  // 패킷 데이터를 넣어 메모리 스트림 생성
        //    //{
        //    //    using (MemoryStream zip = new MemoryStream()) // 리턴받을 메모리 스트림 생성
        //    //    {
        //    //        ICSharpCode.SharpZipLib.GZip.
        //    //            .Decompress(ms, zip, false);  // 압축 해제 하여 output 스트림에 저장
        //    //        ret = zip.ToArray();
        //    //    }
        //    //}
        //}

        //public void ExtractGZipSample(string gzipFileName, string targetDir)
        //{

        //    // Use a 4K buffer. Any larger is a waste.    
        //    byte[] dataBuffer = new byte[4096];

        //    using (System.IO.Stream fs = new FileStream(gzipFileName, FileMode.Open, FileAccess.Read))
        //    {
        //        using (GZipInputStream gzipStream = new GZipInputStream(fs))
        //        {

        //            // Change this to your needs
        //            string fnOut = Path.Combine(targetDir, Path.GetFileNameWithoutExtension(gzipFileName));

        //            using (FileStream fsOut = File.Create(fnOut))
        //            {
        //                StreamUtils.Copy(gzipStream, fsOut, dataBuffer);
        //            }
        //        }
        //    }
        //}

    }
}
