Compatibility = 
{
	["body"] = 
	{
		["motherboard"] = 
		{
			["body"] = "Formfactor",
			["motherboard"] = "Formfactor",
			["condition"] = "contain"
		},
		["videocard"] = 
		{
			["body"] = "VideocardMaxLength",
			["videocard"] = "Length",
			["condition"] = "greater or equal"
		}
	},
	["charger"] = 
	{
		["motherboard"] = 
		{
			["charger"] = 
			{
				"MotherboardConnector",
				"ConnectorType"
			},
			["motherboard"] = 
			{
				"Pin",
				"CPUPin"
			},
			["condition"] = 
			{
				"equal",
				"equal"
			},
		},
		["videocard"] = 
		{
			["charger"] = "VideocardConnector",
			["videocard"] = "Pin",
			["condition"] = "equal"
		}
	},
	["cooler"] = 
	{
		["cpu"] = 
		{
			["cooler"] = "Socket",
			["cpu"] = "Socket",
			["condition"] = "contain"
		},
		["motherboard"] = 
		{
			["cooler"] = "Socket",
			["motherboard"] = "Socket",
			["condition"] = "contain"
		}
	},
	["cpu"] = 
	{
		["cooler"] = 
		{
			["cpu"] = "Socket",
			["cooler"] = "Socket",
			["condition"] = "contained"
		},
		["motherboard"] = 
		{
			["cpu"] = "Socket",
			["motherboard"] = "Socket",
			["condition"] = "equal"
		}
	},
	["hdd"] = 
	{
		["motherboard"] =
		{
			["hdd"] = "Interface",
			["motherboard"] = "Slots",
			["condition"] = "intersect"
		}
	},
	["motherboard"] = 
	{
		["body"] = 
		{
			["body"] = "Formfactor",
			["motherboard"] = "Formfactor",
			["condition"] = "contained"
		},
		["charger"] = 
		{
			["charger"] = 
			{
				"MotherboardConnector",
				"ConnectorType"
			},
			["motherboard"] = 
			{
				"Pin",
				"CPUPin"
			},
			["condition"] = 
			{
				"equal",
				"equal"
			},
		},
		["cooler"] = 
		{
			["cooler"] = "Socket",
			["motherboard"] = "Socket",
			["condition"] = "contained"
		},
		["cpu"] = 
		{
			["motherboard"] = "Socket",
			["cpu"] = "Socket",
			["condition"] = "equal"
		},
		["hdd"] = 
		{
			["motherboard"] = "Slots",
			["hdd"] = "Interface",
			["condition"] = "intersect"
		},
		["ram"] = 
		{
			["motherboard"] = "MemoryType",
			["ram"] = "MemoryType",
			["condition"] = "equal"
		},
		["ssd"] = 
		{
			["motherboard"] = "Slots",
			["ssd"] = "Interface",
			["condition"] = "intersect"
		}
	},
	["ram"] = 
	{
		["motherboard"] = 
		{
			["ram"] = "MemoryType",
			["motherboard"] = "MemoryType",
			["condition"] = "equal"
		}
	},
	["ssd"] = 
	{
		["motherboard"] = 
		{
			["ssd"] = "Interface",
			["motherboard"] = "Slots",
			["condition"] = "intersect"
		}
	},
	["videocard"] = 
	{
		["body"] = 
		{
			["body"] = "VideocardMaxLength",
			["videocard"] = "Length",
			["condition"] = "less or equal"
		},
		["charger"] = 
		{
			["charger"] = "VideocardConnector",
			["videocard"] = "Pin",
			["condition"] = "equal"
		}
	}
}

return Compatibility