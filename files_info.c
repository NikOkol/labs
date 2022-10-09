#include <stdio.h>
#include <sys/types.h>
#include <stdlib.h>
#include <unistd.h>
#include <time.h>
#include <dirent.h>
#include <string.h>
#include <sys/stat.h>
#include <pwd.h>

int info_output(const char * _dirName)
{
	DIR * directory = opendir(_dirName);
	struct stat mstat;
	struct tm lt;
	struct dirent* cdirectory;
	struct passwd *password;
	if (!directory)
	{
		return 0;
	}
	
	while ((cdirectory = readdir(directory)))
	{
		char rd[512];
		snprintf(rd, sizeof rd, "%s/%s", _dirName, cdirectory->d_name);
		if ((stat(rd, &mstat)) == 0)
		{
			password = getpwuid(mstat.st_uid);
		}
		else
		{
			break;
		}
		
		time_t t = mstat.st_mtime;
		localtime_r(&t, &lt);
		char timebuf[80];
		strftime(timebuf, sizeof(timebuf), "%c", &lt);
		if (password != 0)
		{
			printf("%s \t %ld \t %s \t %s", password->pw_name, (long)mstat.st_size, timebuf, cdirectory->d_name);
			printf("\n");
		}
		else
		{
			printf("%d \t %ld \t %s \t %s", mstat.st_uid, (long)mstat.st_size, timebuf, cdirectory->d_name);
			printf("\n");
		}
		
	
	}
	closedir(directory);
	return 0;
}


int main(int argc, char* argv[])
{
	if (argc == 1)
	{
		return info_output(".");
	}
	return info_output(argv[1]);

}




