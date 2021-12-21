\ Author: nyabla
\ Copyright (C) 2021 nyabla.net

\ This file is part of rpi-gpio

\ rpi-gpio is free software; you can redistribute it and/or
\ modify it under the terms of the GNU General Public License
\ as published by the Free Software Foundation, either version 3
\ of the License, or (at your option) any later version.

\ This program is distributed in the hope that it will be useful,
\ but WITHOUT ANY WARRANTY; without even the implied warranty of
\ MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
\ GNU General Public License for more details.

\ You should have received a copy of the GNU General Public License
\ along with this program. If not, see http://www.gnu.org/licenses/.

\ Mode Options
%000 Constant IN
%001 Constant OUT
%100 Constant ALT0
%101 Constant ALT1
%110 Constant ALT2
%111 Constant ALT3
%011 Constant ALT4
%010 Constant ALT5

\ Value Options
0 Constant LOW
1 Constant HIGH

\ Offsets
$0000 Constant FSEL_OFFSET
$001c Constant SET_OFFSET
$0028 Constant CLR_OFFSET
$0034 Constant PINLEVEL_OFFSET
$0040 Constant EVENT_DETECT_OFFSET
$004c Constant RISING_ED_OFFSET
$0058 Constant FALLING_ED_OFFSET
$0064 Constant HIGH_DETECT_OFFSET
$0070 Constant LOW_DETECT_OFFSET
$0094 Constant PULLUPDN_OFFSET
$0098 Constant PULLUPDNCLK_OFFSET

\ gpio address global
0 Value gpio-addr

include c-imports.fs

: gpio-init ( -- )
  s" /dev/gpiomem" r/w bin open-file throw dup
  fileno dup ?ior

  0 swap &180 swap PROT_RW swap MAP_SHARED swap 0 mmap
  dup ?ior
  TO gpio-addr
  close-file throw ;

: gpio-bye ( -- )
  gpio-addr &180 munmap ?ior ;

: gpio-set-mode { pin mode -- }
  pin 10 / cells gpio-addr + FSEL_OFFSET + \ addr
  dup
  pin 10 mod 3 * \ shift
  dup
  rot
  @ swap 7 swap lshift invert and \ *addr & (7 << shift)
  swap mode swap lshift or \ _ | (mode << shift)
  swap ! ;

: gpio-get-mode { pin -- mode }
  pin 10 mod 3 * \ shift
  pin 10 / cells gpio-addr + FSEL_OFFSET + \ addr
  @ swap rshift 7 and ;

: gpio-write { pin out -- }
  out
  IF \ addr if HIGH
    pin 32 / cells gpio-addr + SET_OFFSET +
  ELSE \ addr if LOW
    pin 32 / cells gpio-addr + CLR_OFFSET +
  THEN
  pin 32 mod \ shift
  1 swap lshift
  swap ! ;

: gpio-read { pin -- reading }
  pin 32 / cells gpio-addr + PINLEVEL_OFFSET + \ addr
  1 pin 32 mod lshift \ mask
  swap
  @ and
  IF
    HIGH
  ELSE
    LOW
  THEN ;

gpio-init
