﻿#!/bin/bash

docker compose -f docker-compose.yml -f docker-compose.override.yml up -d --build
sleep 15