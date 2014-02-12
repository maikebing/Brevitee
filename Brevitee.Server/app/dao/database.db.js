var database = {
    nameSpace: "Brevitee.Application.Data",
    schemaName: "BreviteeApplication",
	xrefs: [
		["ApplicationXrefLeft", "ApplicationXrefRight"]
	],
    tables: [
        {
            name: "ApplicationItem",
            fks: [],
            cols: [
                { Name: "String", Null: false }
            ]            
        },
        {
            name: "ApplicationSubItem",
            fks: [
                { ApplicationItemId: "ApplicationItem" }
            ],
            cols:[
                { Name: "String", Null: false },
                { Created: "DateTime" }
            ]
        },
		{
			name: "ApplicationXrefLeft",
			fks: [],
			cols:[
				{ Name: "String", Null: false }
			]
		},
		{
			name: "ApplicationXrefRight",
			fks: [],
			cols:[
				{ Name: "String", Null: false }
			]
		}
    ]
}