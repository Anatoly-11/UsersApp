CREATE TABLE "Street_types"(
	"Id"	  INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT,
	"Name"    TEXT(24) NOT NULL UNIQUE,
	"Short"   TEXT(6) NOT NULL UNIQUE
);

INSERT INTO Street_types(Name, Short) VALUES('Aллея', 'ал'),('Бульвар', 'бул'),('Деревня', 'дер'),('Квартал', 'кврт'),('Линия', 'лин'),('Микрорайон', 'мкрн'),
('Мостовая','мст'),('Набережная','наб'),('Парк','прк'),('Вал','вал'),('Взвоз','взв'),('Линия','лин'),('Луч','луч'),('Улица','ул'),('Переулок','пер'),
('Площадь','пл'),('Проезд','прзд'),('Проектируемый проезд','прпрзд'),('Просека','прск'),('Проспект','пр'),('Тупик','туп'),('Шоссе','ш'),('Проулок','пру'),
('Завоз','зав'),('Заезд','зезд'),('Кольцо','клц'),('Магистраль','маг'),('Перспектива','прсп'),('Разъезд','рзд'),('Спуск','спс'),('Съезд','сзд')

CREATE TABLE "Location_types"(
	"Id"	  INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT,
	"Name"    TEXT(24) NOT NULL UNIQUE,
	"Short"   TEXT(6) NOT NULL UNIQUE
);

INSERT INTO Location_types(Name, Short) VALUES('Город', 'г'),('Посёлок городского типа', 'пгт'),('Деревня', 'дер'),('Станция', 'стн'),('Станица', 'стц'),('Разъезд', 'рзд'),
('Аул','ау'),('Хутор','хут'),('Кишлак','киш'),('Починок','поч'),('Заимка','зим'),('Погост','пог'),('Слобода','слб'),('Выселок','выс'),('Околоток','окл');

CREATE TABLE "Adresses"(
	"Id"	     INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT,
	"Post_Index" INTEGER NOT NULL,
	"Location"	 TEXT(32) NOT NULL,
	"Loc_Id"	 INTEGER NOT NULL,
	"Street"	 TEXT(48) NOT NULL, 
	"Stt_Id"	 INTEGER NOT NULL, 
	"Nhouse"	 INTEGER NOT NULL,
	"Nshared"	 INTEGER,
	"Build"	     INTEGER,
	"Ftat"	     INTEGER,
	FOREIGN KEY("Loc_Id") REFERENCES "Location_types"("Id"),
	FOREIGN KEY("Stt_Id") REFERENCES "Street_types"("Id")
);

CREATE TABLE "Spouses"(
	"Id"	INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT,
	"Name"	     TEXT(32) NOT NULL,
	"Family"	 TEXT(32) NOT NULL,
	"Patronic"	 TEXT(32) NOT NULL,
	"Loc_Id"	 INTEGER NOT NULL,  -- Адресс прописки 
	"Birthday"	 TEXT(32) NOT NULL, -- Дата рождения
	"Brn_Id"	 INTEGER NOT NULL,  -- Место рождения  
	"work_position"	 TEXT(32) NOT NULL, -- Место работы должность
);

CREATE TABLE "Children"(
	"Id"	INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT,
	"Name"	     TEXT(32) NOT NULL,
	"Family"	 TEXT(32) NOT NULL,
	"Patronic"	 TEXT(32) NOT NULL,
	"Gender"     TEXT(3)  NOT NULL CHECK('муж' | 'жен'),
	"Loc_Id"	 INTEGER NOT NULL,  -- Адресс прописки 
	"Birthday"	 TEXT(32) NOT NULL, -- Дата рождения
	"Brn_Id"	 INTEGER NOT NULL,  -- Место рождения  
	"Usr_ID"     INTEGER NOT NULL,  -- чьи  дети 
	FOREIGN KEY("Usr_ID") REFERENCES "Users"("Id")
);

CREATE TABLE "Passports"(
	"Id"	INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT,
	"Name"	     TEXT(32) NOT NULL,
	"Family"	 TEXT(32) NOT NULL,
	"Patronic"	 TEXT(32) NOT NULL,
	"Number"     INTEGER NOT NULL,  -- Номер паспорта
	"Seria"      INTEGER NOT NULL,  -- Серия паспорта
	"Who_get"    TEXT(256) NOT NULL, -- Кем выдан 
	"Date_get"   TEXT(32) NOT NULL,  -- Когда выдан
	"Div_Cod"    TEXT(16) NOT NULL,  -- Код подразделения
	"Gender"     TEXT(3)  NOT NULL CHECK('муж' | 'жен'),
	"Photo"      BLOB  NOT NULL,
	"Birthday"	 TEXT(32) NOT NULL, -- Дата рождения
	"Brn_Id"	 INTEGER NOT NULL,  -- Место рождения  
	"Loc_Id"	 INTEGER NOT NULL,  -- Адресс прописки 
	FOREIGN KEY("Loc_Id") REFERENCES "Adresses"("Id"),
	FOREIGN KEY("Brn_Id") REFERENCES "Adresses"("Id")
);

CREATE TABLE "Users" (
	"Id"	INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT,
	"Phone"	    TEXT(32) NOT NULL,
	"Pass_Id"    INTEGER  NOT NULL,  -- удостоверение личности
	"Aloc_Id"   INTEGER NOT NULL, -- Фактический адресс проживания
	"Login"	TEXT(32) NOT NULL UNIQUE,
	"Pswd"	BLOB(32) NOT NULL,
	"Email"	TEXT(32) NOT NULL UNIQUE
	"Spouse_Id"  INTEGER NULL,  -- данные о супруге
	FOREIGN KEY("Spouse_Id") REFERENCES "Spouses"("Id"),
	FOREIGN KEY("Pass_Id") REFERENCES "Passports"("Id"),
	FOREIGN KEY("Aloc_Id") REFERENCES "Adresses"("Id"),
	FOREIGN KEY("Brn_Id") REFERENCES "Adresses"("Id")
);

INSERT INTO Users("Name", "Family",	"Patronic", "Photo", "Phone",  "Birthday",  "Loc_Id",  "Brn_Id",  "Login", "Pswd", "Email") VALUES
('Petrov', 'qwerty', 'ipiter@mail.ru'),
('Sidorov', '1(*3idi', 'sidor@gmail.ru'),
('Fialkin', '*#49uieuio', 'fialka@yandex.ru'),
('Hilovsky', 'EUi82783', 'nilovs@mail.ru'),
('Nazarov', '_oweo?290', 'nazar@yandex.ru');

select last_insert_rowid() as ID
