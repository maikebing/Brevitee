var database = {
    nameSpace: "Brevitee.Distributed.Files",
    schemaName: "DistributedFiles",
    xrefs: [        
    ],
    tables: [
        {
            name: "Node",
            fks: [
                { ParentId: "Node" }
            ],
            cols: [
                { Name: "String" },
                { Container: "Boolean" },
                { StartChunkId: "Long" }
            ]
        },
        {
            name: "Chunk",
            fks: [
                { PreviousChunkId: "Chunk" },
                { NextChunkId: "Chunk" }
            ],
            cols: [
                { Data: "Byte" },                
            ]
        }
    ]
};
