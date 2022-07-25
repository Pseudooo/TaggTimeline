
CREATE TABLE IF NOT EXISTS "Categories" (
    "Id" uuid NOT NULL,
    "CreatedDate" timestamp with time zone NOT NULL,
    "ModifiedDate" timestamp with time zone NULL,
    "DeletedDate" timestamp with time zone NULL,
    "Key" text NOT NULL,
    CONSTRAINT "PK_Categories" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "CategoryTagg" (
    "CategoriesId" uuid NOT NULL,
    "TaggsId" uuid NOT NULL,
    CONSTRAINT "PK_CategoryTagg" PRIMARY KEY ("CategoriesId", "TaggsId"),
    CONSTRAINT "FK_CategoryTagg_Categories_CategoriesId" FOREIGN KEY ("CategoriesId") REFERENCES "Categories" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CategoryTagg_Taggs_TaggsId" FOREIGN KEY ("TaggsId") REFERENCES "Taggs" ("Id") ON DELETE CASCADE
);
