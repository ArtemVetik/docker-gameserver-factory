#!/bin/bash

chmod a+x /root/build/serverbuild.x86_64 \
xvfb-run -ae /dev/stdout \
    --server-args='-screen 0 640x480x24:32' \
        /root/build/serverbuild.x86_64 \
            -batchmode \
            -nographics \
            -logfile /dev/stdout
