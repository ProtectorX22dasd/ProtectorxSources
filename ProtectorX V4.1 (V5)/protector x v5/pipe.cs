using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;

namespace protector_x_v5
{
	// Token: 0x02000006 RID: 6
	public class Pipe
	{
		// Token: 0x06000015 RID: 21
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool WaitNamedPipe(string pipe, int timeout = 10);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022D1 File Offset: 0x000004D1
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000022D9 File Offset: 0x000004D9
		public string Name { get; set; }

		// Token: 0x06000018 RID: 24 RVA: 0x000022E2 File Offset: 0x000004E2
		public Pipe(string n)
		{
			this.Name = n;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022F1 File Offset: 0x000004F1
		public bool Exists()
		{
			return Pipe.WaitNamedPipe("\\\\.\\pipe\\" + this.Name, 10);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000230C File Offset: 0x0000050C



		public string Read()
		{
			if (this.Name == null)
			{
				throw new Exception("Pipe Name was not set.");
			}
			if (this.Exists())
			{
				using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", this.Name, PipeDirection.InOut))
				{
					namedPipeClientStream.Connect();
					using (StreamReader streamReader = new StreamReader(namedPipeClientStream))
					{
						return streamReader.ReadToEnd();
					}
				}
			}
			return "";
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002394 File Offset: 0x00000594
		public bool Write(string content)
		{
			if (this.Name == null)
			{
				throw new Exception("Pipe Name was not set.");
			}
			if (string.IsNullOrWhiteSpace(content) || string.IsNullOrEmpty(content))
			{
				return false;
			}
			if (this.Exists())
			{
				using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", this.Name, PipeDirection.InOut))
				{
					namedPipeClientStream.Connect();
					using (StreamWriter streamWriter = new StreamWriter(namedPipeClientStream))
					{
						streamWriter.Write(content);
					}
					return true;
				}
				return false;
			}
			return false;
		}
	}
}
