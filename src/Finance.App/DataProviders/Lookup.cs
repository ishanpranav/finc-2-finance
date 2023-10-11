// Lookup.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Finance.App;

public delegate double Lookup(SecurityTable table, int row, int security);
