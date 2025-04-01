using System;
using System.Linq;
using System.Collections.Generic;

class Pass1 {

	public static void Main() {
		int memLoc = 0;
		int lineNo = 1;
		string fileName = "";
		string delCom, pushst, buf;
		string[] lines = {""}, split;
		List<string> hex = new List<string>();
		Dictionary<int, string[]> memory = new Dictionary<int, string[]>();
		Dictionary<int[], string> labels = new Dictionary<int[], string>();

		Console.WriteLine("Enter a Filename");

		try {
			fileName = Console.ReadLine() ?? "";

			lines = File.ReadAllLines(fileName);
		} catch {
			Console.WriteLine($"{fileName} does not exist");
		}


		foreach (string line in lines)
		{
			delCom = line.Split('#')[0].Trim();
			if (delCom == "") {
				continue;
			}

			split = delCom.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			if (split[0] != "stpush") {
				if (split[0].Contains(':')) {
					labels.Add(new int[] {memLoc, lineNo} , split[0].Substring(0, split[0].Length-1));
				} else {
					memory.Add(memLoc, split);
					memLoc += 4;
				}
			} else {
				pushst = string.Join(' ', split.Skip(1)).Trim('"');

				pushst = pushst.Replace("\\\\", "\\");
				pushst = pushst.Replace("\\\"", "\"");
				pushst = pushst.Replace("\\\n", "|");
				Console.WriteLine(pushst);

				buf = "";
				for (int i = 0; i < pushst.Length; i++) {
					if (pushst[i] == '|') buf = ((int)'|').ToString("X") + buf;
					else buf = ((int)pushst[i]).ToString("X") + buf;
					
					if (i % 3 == 2 && i != pushst.Length-1) {
						buf = "0x01" + buf;
						/* Console.WriteLine(buf); */
						hex.Add(buf);
						buf = "";
					} else if (i == pushst.Length-1) {
						while(buf.Length < 8) {
							buf = "00" + buf;
						}
						buf = "0x" + buf;
						/* Console.WriteLine(buf); */
						hex.Add(buf);
						buf = "";
					}
				}

				hex.Reverse();
				foreach (string cmd in hex) {
					memory.Add(memLoc, new string[] {"push", cmd});
					memLoc += 4;
				} 
				hex.Clear();
			}

			lineNo++;
				
			/* foreach (string liner in split){ */
    			/* Console.WriteLine(liner); */
			/* } */

		}

		foreach (var entry in memory) {
			Console.WriteLine($"{entry.Key}: {string.Join(" ", entry.Value)}");
		}

		foreach (var entry in labels) {
			Console.WriteLine($"{entry.Key[0]}: {entry.Value} {entry.Key[1]}");
		}
	}
}
