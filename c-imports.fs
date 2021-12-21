\ Authors: Bernd Paysan, Anton Ertl, nyabla
\ Copyright (C) 1998,2000,2003,2005,2006,2007,2008,2009,2010,2011,2013,2014,2015,2016,2019 Free Software Foundation, Inc.
\ Copyright (C) 2021 nyabla.net

\ This file is part of rpi-gpio
\ Parts of this file contain code adapted from mmap.fs, part of Gforth
\ The source can be found at
\ https://git.savannah.gnu.org/cgit/gforth.git/tree/unix/mmap.fs

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

c-library gpio-deps
  \c #include <stdio.h>
  c-function fileno fileno a -- n ( file* -- fd )

  \c #include <errno.h>
  \c #define errno_wrap() errno
  c-function errno errno_wrap -- n ( -- value )

  \c #include <sys/mman.h>
  c-function mmap mmap a n n n n n -- a ( addr len prot flags fd off -- addr )
  c-function munmap munmap a n -- n ( addr len - ior )
end-c-library

: ?errno-throw ( f -- )
    \ throw code computed from errno if f != 0
    IF -512 errno - throw THEN ;

: ?ior ( x -- )
    \ use errno to generate throw when failing
    -1 = ?errno-throw ;

\ constants for mmap
$3 Constant PROT_RW
$1 Constant MAP_SHARED
