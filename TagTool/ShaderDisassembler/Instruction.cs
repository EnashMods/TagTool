﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagTool.ShaderDisassembler
{
	public class Instruction
	{
		public ControlFlowInstruction cfInstr;
		public ALUInstruction aluInstr;
		public FetchInstruction fetchInstr;

		public InstructionType instructionType;

		public Instruction(ControlFlowInstruction cf_instr)
		{
			this.cfInstr = cf_instr;
			this.instructionType = InstructionType.CF;
		}

		public Instruction(ALUInstruction instr)
		{
			this.aluInstr = instr;
			this.instructionType = InstructionType.ALU;
		}

		public Instruction(FetchInstruction instr)
		{
			this.fetchInstr = instr;
			this.instructionType = InstructionType.FETCH;
		}

		public Instruction() { }
	}

	public enum InstructionType
	{
		UNKNOWN,
		CF,
		ALU,
		FETCH
	}
}