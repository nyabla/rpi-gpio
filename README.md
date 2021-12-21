# rpi-gpio

_rpi-gpio is currently in development and therefore unstable_

rpi-gpio is a basic library that aims to provide a simple and useful Forth interface for
basic GPIO interactions on the Raspberry Pi platform that is written, as much
as possible, in Forth.

rpi-gpio currently uses only the `/dev/gpiomem` character device, for access
without sudo and bypassing the complex new kernel API. Due to this analog
operations are not yet supported.

## Compatibility

Raspberry Pi models based on BCM2835, BCM2836, BCM2837, and BCM2837B0 should
work without issue. The models covered by this are:
* Raspberry Pi 1 Model B/A+/B+
* Raspberry Pi 2 Model B (tested)
* Raspberry Pi 3 Model B/A+/B+
* Raspberry Pi Zero/Zero W/Zero 2 W

The Raspberry Pi 4 uses the new BCM2711, in which the register layout is
different for the pull-up/pull-down registers, so you should be fine using
functions for setting and getting mode, digital reading and writing, and event
detection.

## Documentation

Functions will be documented when some sort of stability is reached.

## Attributions and Acknowledgements

* `?ior` and `?errno-throw` in `c-imports.fs` are lifted from mmap.fs, authored by Bernd Paysan
  and Anton Ertl, part of Gforth.

