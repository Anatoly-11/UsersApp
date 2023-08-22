CREATE TABLE "Users" (
	"Id"	INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT,
	"Login"	TEXT(32) NOT NULL UNIQUE,
	"Pswd"	TEXT(32) NOT NULL,
	"Email"	TEXT(32) NOT NULL UNIQUE
);

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
	"Id"	    INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT,
	"Post_Index"	INTEGER NOT NULL,
	"Location"	TEXT(32) NOT NULL,
	"Loc_Id"	INTEGER NOT NULL,
	"Street"	TEXT(48) NOT NULL, 
	"Stt_Id"	INTEGER NOT NULL, 
	"Nhouse"	INTEGER NOT NULL,
	"Nshared"	INTEGER,
	"Build"	    INTEGER,
	"Ftat"	    INTEGER,
	FOREIGN KEY("Loc_Id") REFERENCES "Location_types"("Id"),
	FOREIGN KEY("Stt_Id") REFERENCES "Street_types"("Id")
);

CREATE TABLE "Users_Date"(
	"Id"	    INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT,
	"Usr_Id"	INTEGER NOT NULL,
	"Name"	    TEXT(32) NOT NULL,
	"Family"	TEXT(32) NOT NULL,
	"Patronic"	TEXT(32) NOT NULL,
	"Phone"	    TEXT(32) NOT NULL,
	"Post_Index" INTEGER NOT NULL,
	"Loc_Id"	INTEGER NOT NULL,
	"Birthday"	TEXT(32) NOT NULL,
	"Brn_Id"	INTEGER NOT NULL,
	FOREIGN KEY("Loc_Id") REFERENCES "Adresses"("Id"),
	FOREIGN KEY("Brn_Id") REFERENCES "Adresses"("Id"),
	FOREIGN KEY("Usr_Id") REFERENCES "Users"("Id")
);

INSERT INTO Users(Login, Pswd, Email) VALUES
('Petrov', 'qwerty', 'ipiter@mail.ru'),
('Sidorov', '1(*3idi', 'sidor@gmail.ru'),
('Fialkin', '*#49uieuio', 'fialka@yandex.ru'),
('Hilovsky', 'EUi82783', 'nilovs@mail.ru'),
('Nazarov', '_oweo?290', 'nazar@yande.ru');
