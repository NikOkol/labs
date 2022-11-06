#include <iostream>
#include <sys/stat.h>
#include <cstring>
#include <fcntl.h>
#include <unistd.h>

using namespace std;

int main(int argc, char* argv[], char *envp[])
{
	int fd;
	(void) umask(0);
	char b_sz;
	size_t b_size = 0;
	
	if ((fd = open("text.txt", O_RDONLY)) < 0)
	{
		cout << "File open failed!" << endl;
		exit(-1);
	}
	else
	{
		while ((b_size = read(fd, &b_sz, 1)) > 0)
		{
			putchar(b_sz);
		}
		cout << endl;
	}
	
	if (b_size < 0)
	{
		cout << "File read failed!" << endl;
		exit(-1);
	}
	
	if (close(fd) < 0)
	{
		cout << "File close failed!" << endl;
	}
	
	return 0;
}

