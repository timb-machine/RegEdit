/*
$Header: /var/lib/cvsd/var/lib/cvsd/RegEdit/CLI.cs,v 1.2 2012-10-30 17:01:50 timb Exp $

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation
and/or other materials provided with the distribution.
* Neither the name of the Nth Dimension nor the names of its contributors may
be used to endorse or promote products derived from this software without
specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
POSSIBILITY OF SUCH DAMAGE.

(c) Tim Brown, 2009
<mailto:timb@nth-dimension.org.uk>
<http://www.nth-dimension.org.uk/> / <http://www.machine.org.uk/>
*/

using System;
using Microsoft.Win32;

namespace RegEdit
{
	/// <summary>
	/// Commandline interface.
	/// </summary>
	class CLI
	{
		/// <summary>
		/// Parses and handle the configuration.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			RegistryKey registryhive, registrykey;
			if (args.Length < 4)
			{
				Console.Error.WriteLine("Usage: RegEdit -r|-w <hive> <key> <value> [<data>]");
				return;
			}
			switch (args[0]) 
			{
				case "-r":
					if (args.Length < 3) 
					{
						Console.Error.WriteLine("Usage: RegEdit -r <hive> <key> <value>");
						return;
					}
					switch(args[1]) 
					{
						case "HKEY_CURRENT_USER":
							registryhive = Registry.CurrentUser;
							break;
						case "HKEY_LOCAL_MACHINE":
							registryhive = Registry.LocalMachine;
							break;
						default:
							Console.Error.WriteLine("Invalid hive");
							return;
					}
					registrykey = registryhive.CreateSubKey(args[2]);
					Console.WriteLine(registrykey.GetValue(args[3]));
					break;
				case "-w":
					if (args.Length < 5)
					{
						Console.Error.WriteLine("Usage: RegEdit -w <hive> <key> <value> [<data>]");
						return;
					}
					switch(args[1]) 
					{
						case "HKEY_CURRENT_USER":
							registryhive = Registry.CurrentUser;
							break;
						case "HKEY_LOCAL_MACHINE":
							registryhive = Registry.LocalMachine;
							break;
						default:
							Console.Error.WriteLine("Invalid hive");
							return;
					}
					registrykey = registryhive.CreateSubKey(args[2]);
					registrykey.SetValue(args[3], args[4]);
					break;
				default:
					Console.Error.WriteLine("Usage: RegEdit -r|-w <hive> <key> <value> [<data>]");
					return;
			}
		}
	}
}
