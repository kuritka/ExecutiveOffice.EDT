using ExecutiveOffice.EDT.FileOps.Common;
using System.Collections.Generic;
using System.IO;

namespace ExecutiveOffice.EDT.FileOps.Processors.Compression
{
	internal interface ICompressor
	{
        OneOrZeroElementCollection<FileInfo> Compress(IEnumerable<FileInfo> filesToCompress, FileInfo compressedFileName);

	    IEnumerable<FileInfo> Decompress(FileInfo compressedFile, DirectoryInfo decompressionDirectory = null);
	}
}
