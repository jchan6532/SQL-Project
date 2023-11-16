CREATE TABLE DefaultSettings (
	config_key NVARCHAR(60) PRIMARY KEY NOT NULL,
	int_value INT,
	float_value FLOAT,
	string_value NVARCHAR(60)
)

CREATE TABLE ConfigSettings (
	config_key NVARCHAR(60) PRIMARY KEY FOREIGN KEY REFERENCES DefaultSettings(config_key) NOT NULL,
	int_value INT,
	float_value FLOAT,
	string_value NVARCHAR(60)
)