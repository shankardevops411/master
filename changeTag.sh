#!/bin/bash
sed "s/tagversion/$1/g" scheduleservice.yaml > scheduleservice.yaml
