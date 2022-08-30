# VMPROTECT

<img src="https://repository-images.githubusercontent.com/236199786/eab7cc00-05b7-11eb-9f91-6ea165c2cc2c" height="200">

<i>A code obfuscation method using virtual machines to protect programs</i>

<a href="https://github.com/eaglx/VMPROTECT/stargazers"><img src="https://img.shields.io/github/stars/eaglx/VMPROTECT" alt="Stars Badge"/></a>
<a href="https://github.com/eaglx/VMPROTECT/network/members"><img src="https://img.shields.io/github/forks/eaglx/VMPROTECT" alt="Forks Badge"/></a>
<a href="https://github.com/eaglx/VMPROTECT/blob/master/LICENSE"><img src="https://img.shields.io/github/license/eaglx/VMPROTECT?color=2b9348" alt="License Badge"/></a>
[![GitHub release](https://img.shields.io/github/release/eaglx/VMPROTECT)](https://GitHub.com/eaglx/VMPROTECT/releases/)
![Progress](https://progress-bar.dev/5/?title=progress-v1.0)

A virtual machine that simulates a CPU along with a few other hardware components, allows to perform arithmetic operations, reads and writes to memory and interacts with I/O devices. It can understand a machine language which can be used to program it. Virtual machines used in code obfuscation are completely different than common virtual machnines. They are very specific to the task of executing a few set of instructions. Each instruction is given a custom opcode (often generated at random).

## Table of contents
* [Requirements](#requirements)
* [Editor](#editor)
* [Compiler](#compiler)
* [Debugger](#debugger)
* [VMCore](#vmcore)
  * [Args](#args)
  * [Security](#security)
  * [Documentation](#documentation)
    * [Memory](#memory)
    * [Drivers](#drivers)
      * [Sysbus](#sysbus)
    * [Registers](#registers)
    * [Instructions](#instructions)
* [Disclaimer](#disclaimer)

## Requirements
* NASM [tested on 2.13.02]

## Editor

tbd


## Compiler
The *nasm* as compiler is used for compilation a code. Remember to include the *vm.inc* file with definitions of opcodes in your programs. Additionally, at the beginning of the code should be included magic number *0x566d*. An example program for virtual machine below.

```nasm
%include "vm.inc"   ; Or full path to this file!

start:
    dw 0x6d56
    movd r0, 0x5
    advrd r0, 0x5
    push r0
    pxvn
    ee
```

## Debugger
tbd

## VMCore
### Args
tbd

### Security
tbd


### Documentation
The VM will simulate a fictional cpu (32-bit). It has a custom instrucion set compared to x86-64.

#### Memory
The VM has 51,200 memory locations, each of which stores a 8-bit value (it can store a total of 50kb). The VM has stack, which is a separate data structure. The stack has 256 memory locations, each of which stores a 32-bit value (it can store a total of 512b).

Also, there is a data buffer which has 1024 memory locations, each of which stores a 1-bit value. This buffer will store user input.


In addition, the *VMCore* has implemented memory management through memory frames. This allows the execution of programs larger than those specified by CODE_DATA_SIZE. The frames will be saved in files named *.cached.x.frame* where *x* is the frame number.

#### Drivers
Drivers are designed to expand the *VMPROTECT*'s capabilities.

OPCODE | Mnemonic and params | Description
--- | --- | ---
60  |  VMSYSBUS word | Arguments to functions pass via the stack |

#### Sysbus
A sysbus is a driver that allows access to a filesystem. Arguments to functions pass via the stack.

FUNC | CMD | CODE | Windows | Linux | MacOS
--- | --- | --- | --- | --- | ---
createDirectory | sysdircr | 1 | YES | YES | NO |
deleteDirectory | sysdirdel | 2 | YES | YES | NO |
moveDirectory | sysdirmv | 3 | YES | NO | NO |
copyDirectory | sysdircp | 4 | YES | NO | NO |
createFile | sysfilecr | 5 | YES | YES | NO |
deleteFile | sysfiledel | 6 | YES | YES | NO |
moveFile | sysfilemv | 7 | YES | YES | NO |
copyFile | sysfilecp | 8 | YES | YES | NO |

Functions' implementation:


Example call function use case:
```nasm
%include "vm.inc"

start:
    dw 0x6d56
    movd r1, 0x0
    movd r2, 0x0
    movd r1, path
    movd r2, data
    push r1
    push r2
    vmsysbus sysfilecr
    ee

data:
    db 0x01, 0x02, 0x03, 0x04, 0x3, 0xD
path:
    db "/home/eaglx/file.bin", 0x3, 0xD
```

#### Registers
A register is a slot for storing value on the CPU. The VM has 10 total registers, each of which is 4 bytes (32 bits). The six of them are general purpose, one has designated role as program counter and another has role as stack pointer. The VM has also two regisers ZF (Zero Flag) and CF (Carry Flag). These two provide information about the most recently executed calculation (allows to check logical conditions such as *AND*).


#### Instructions
An instruction is a command which tells the CPU (and the VM's cpu) to do some task, such compare two values. Instructions have both an opcode which indicates the kind of task to perform and a set of parameters which provide inputs to the task being performed.

```assembly
ADRR R2, R1 => 22 02 01
```

<details>
  <summary>Show full list of opcodes</summary>

OPCODE | Mnemonic and params | Description
--- | --- | ---
00  | NOP | No operation |
EE  | EE | End of code and end of the VM's cpu |
01  | MOV r<sub>dst</sub>, r<sub>src</sub> | Move from a register to a register|
02  |  MOVMB r<sub>dst</sub>, addr<sub>src</sub> | Move and extend byte from memory to a register|
03  |  MOVMW r<sub>dst</sub>, addr<sub>src</sub> | Move word from memory to a register |
04  |  MOVB r<sub>dst</sub>, byte | Move and extend byte to a register  |
05  |  MOVW r<sub>dst</sub>, word | Move word to a register |
06  |  MOVBM addr<sub>dst</sub>, r<sub>src</sub> | Move byte from a register to memory location |
07  |  MOVWM addr<sub>dst</sub>, r<sub>src</sub> | Move word from a register to memory location |
08  |  MOVMRB r<sub>dst</sub>, r<sub>src</sub> | Move and extend byte from memory to a register; get an address from a register |
09  |  MOVMRW r<sub>dst</sub>, r<sub>src</sub> | Move word from memory to a register; get an address from a register |
0A  |  MOVMD r<sub>dst</sub>, addr<sub>src</sub> | Move double word from memory to a register |
0B  |  MOVD r<sub>dst</sub>, dword | Move double word to a register |
0C  |  MOVDM addr<sub>dst</sub>, r<sub>src</sub> | Move double word from a register to memory location |
0D  |  MOVMRD r<sub>dst</sub>, r<sub>src</sub> | Move double from memory to a register; get an address from a register |
  | | |
20  |  JMP addr | Unconditional jump |
21  |  JZ addr | Jump if equal; it set up PC to the specified location if the ZF is set (1) |
22  |  JNZ addr | Jump if not equal; it set up PC to the specified location if the ZF is not set (0) |
23  |  JAE addr | Jump if above or equal; it set up PC to the specified location if the ZF is set (1) and the CF is not set (0) |
24  |  JBE addr | Jump if below or equal; it set up PC to the specified location if the ZF is set (1) and the CF is set (1) |
25  |  JB addr | Jump if below; it set up PC to the specified location if the ZF is not set (0) and the CF is set (1) |
26  |  JA addr | Jump if above; it set up PC to the specified location if the ZF is not set (0) and the CF is not set (0) |
  | | |
30  |  ADVR r<sub>dst</sub>, word | Add word value to a register |
31  |  ADRR r<sub>dst</sub>, r<sub>src</sub> | Add two registers |
32  |  ADRRL r<sub>dst</sub>, r<sub>src</sub> | Add two registers (the low byte) |
33  |  SUBVR r<sub>dst</sub>, word | Substract word value from a register |
34  |  SUBRR r<sub>dst</sub>, r<sub>src</sub> | Substract two registers |
35  |  SUBRRL r<sub>dst</sub>, r<sub>src</sub> | Substract two registers (the low byte) |
36  |  XOR r<sub>dst</sub>, r<sub>src</sub> | Xor two registers |
37  |  XOR r<sub>dst</sub>, r<sub>src</sub> | Xor two registers (the low byte) |
38  |  NOT r<sub>dst</sub>| Bitwise NOT on value in a register |
39  |  NOT r<sub>dst</sub> | Bitwise NOT on value in a register (the low byte) |
3A  |  ADVRD r<sub>dst</sub>, dword | Add double word value to a register |
3B  |  SUBVR r<sub>dst</sub>, dword | Substract double word value from a register |
3C  |  SHR r<sub>dst</sub>, count<sub>byte</sub> | Shift the bits of the operand destination to the right, by the number of bits specified in the count operand |
3D  |  SHL r<sub>dst</sub>, count<sub>byte</sub> | Shift the bits of the operand destination to the left, by the number of bits specified in the count operand |
  | | |
50  |  CMP r<sub>dst</sub>, r<sub>src</sub> | Compare two registers |
51  |  CMPL r<sub>dst</sub>, r<sub>src</sub> | Compare two registers (the low byte) |
  | | |
60  |  VMSYSBUS word | Arguments to functions pass via the stack |
  | | |
90  |  PUSH r<sub>src</sub> | Push value from a register to stack |
91  |  POP r<sub>dst</sub> | Pop value from stack to a register |
92  |  CLST | Clear the stack |
93  |  SETSP dword| Set the stack pointer to the double word value |
  | | |
A0  |  POC  | Print char without new line, the value must be at the top of the stack |
A1  |  POCN  | Print char with new line, the value must be at the top of the stack |
A2  |  TIB  | Take input and move to the data buffer, the length of the string is stored in R[7] |
A3  |  GIC r<sub>src</sub> | Get a specific char from input, that is stored in the data buffer, the value will be stored in R[7], pass the position of char via a some register |
A4  |  PIC  | Print char from input without new line, the value must be at the top of the stack |
A5  |  PICN  | Print char from input with new line, the value must be at the top of the stack |
A6  |  PXV  | Print a value in hex, the value must be at the top of the stack |
A7  |  PXVN  | Print a value in hex with a new line, the value must be at the top of the stack |

</details>

## Star History
[![Star History Chart](https://api.star-history.com/svg?repos=eaglx/VMPROTECT&type=Date)](https://star-history.com/#eaglx/VMPROTECT&Date)

---
## Disclaimer
#### VMPROTECT is for EDUCATION and/or RESEARCH purposes only. The author takes NO responsibility and/or liability for how you choose to use this software and damages caused by this software. You bear the full responsibility for your actions.

#### By using this software, you automatically agree to the above.
---
