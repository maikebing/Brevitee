var database = {
    nameSpace: "Brevitee.Common.Data",
    schemaName: "BreviteeCommon",
	xrefs:[
		// [ "LeftTableName": "RightTableName ] // array of arrays that have two values; defines many to many 
	],
    tables: [
        {
            name: "CommonItem",
            fks: [],
            cols: [
                { Name: "String", Null: false }
            ]            
        },
        {
            name: "CommonSubItem",
            fks: [
                { CommonItemId: "CommonItem" }
            ],
            cols:[
                { Name: "String", Null: false },
                { Created: "DateTime" }
            ]
        }
    ]
}