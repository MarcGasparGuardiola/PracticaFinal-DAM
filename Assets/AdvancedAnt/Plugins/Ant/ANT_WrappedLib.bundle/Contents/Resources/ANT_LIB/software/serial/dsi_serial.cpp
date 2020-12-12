/*
This software is subject to the license described in the License.txt file 
included with this software distribution. You may not use this file except in compliance 
with this license.

Copyright (c) Dynastream Innovations Inc. 2013
All rights reserved.
*/

#include "types.h"
#include "dsi_serial.hpp"


//////////////////////////////////////////////////////////////////////////////////
// Public Class Functions
//////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////
// Constructor
/////////////////////////////////////////////////////////////////
DSISerial::DSISerial()
{
   pclCallback = (DSISerialCallback*)NULL;
}

/////////////////////////////////////////////////////////////////
// Destructor
/////////////////////////////////////////////////////////////////
DSISerial::~DSISerial()
{
}

/////////////////////////////////////////////////////////////////
void DSISerial::SetCallback(DSISerialCallback *pclCallback_)
{
   pclCallback = pclCallback_;
}

