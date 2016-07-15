#region license
// Copyright (c) 2016, Wm. Barrett Simms
// 
// Permission to use, copy, modify, and/or distribute this software for any
// purpose with or without fee is hereby granted, provided that the above
// copyright notice and this permission notice appear in all copies.
// 
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
// ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
// ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
// OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectRoller
{
	class Program
	{
		static int Main(string[] args)
		{

			if (args.Length == 0 || (args[0] != "v1" && args[0] != "v2"))
			{
				Usage();
				return -1;
			}
			ValidateArgs(args);

			CodePuller cp = new CodePuller();
			cp.GetCode(args[0], args[1]);
			return 0;
		}

		static void ValidateArgs(string[] arg)
		{
			if (arg[1].Contains(' '))
				throw new ArgumentException("Project cannot contain spaces");
		}

		static void Usage()
		{
			Console.WriteLine("Usage: ProjectRoller <v1|v2> <ProjectName>");
			Console.WriteLine("Example: ProjectRoller v1 MyCoolProject");
		}
	}
}
