var database = {
    nameSpace: "Brevitee.Analytics.Classification",
    schemaName: "Classification",
    xrefs: [

    ],
    tables: [        
        // -- classification tables
        {
            name: "Category",
            cols: [
                { Value: "String", Null: false },
                { DocumentCount: "Long", Null: false }
            ]
        },
        {
            name: "Feature",
            fks: [
                { CategoryId: "Category" }
            ],
            cols: [
                { Value: "String", Null: false },
                { FeatureToCategoryCount: "Long", Null: false }
            ]
        }
        // -- end classification tables
    ]
}
