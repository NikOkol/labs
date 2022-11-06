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
	
	char* b_sz = argv[1];
	size_t b_size = strlen(b_sz);
	
	if ((fd = open("text.txt", O_WRONLY | O_CREAT, 0666)) < 0)
	{
		cout << "File open failed!" << endl;
		exit(-1);
	}
	
	if (write(fd, b_sz, b_size) != b_size)
	{
		cout << "Write failed!" << endl;
		exit(-1);
	}
	
	if (close (fd) < 0)
	{
		cout << "Close failed!" << endl;
	}
	
	return 0;
}
