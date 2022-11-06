#include <iostream>
#include <unistd.h>
#include <sys/stat.h>
#include <cstring>

using namespace std;

int main(void)
{
	int pipe1[2], pipe2[2], id;
	size_t parentStrSize;
	size_t childStrSize;
	char parentStr[128] = {};
	char childStr[128] = {};
	char outputStr[128] = {};
	
	cout << "Enter parent string: ";
	cin.getline(parentStr, 128);
	parentStrSize = strlen(parentStr);
	cout << "Enter child string: ";
	cin.getline(childStr, 128);
	childStrSize = strlen(childStr);
	
	(void)umask(0);
	
	if (pipe(pipe1) < 0 || pipe(pipe2) < 0)
	{
		cout << "Pipe create failed!" << endl;
		exit(-1);
	}
	
	id = fork();
	if (id > 0)
	{
		close(pipe1[0]);
		close(pipe2[1]);
		if (write(pipe1[1], parentStr, parentStrSize) != parentStrSize)
		{
			cout << "Can't write whole string!" << endl;
			exit(-1);
		}
		close(pipe1[1]);
		if (read(pipe2[0], outputStr, 128) < 0)
		{
			cout << "Can't read a string!" << endl;
			exit(-1);
		}
		
		cout << "\nChild's string in parent process: " << outputStr << endl;
		close(pipe2[0]);
		cout << "Parent process terminated" << endl;
	}
	else if (id == 0)
	{
		close(pipe1[1]);
		close(pipe2[0]);
		
		if (read(pipe1[0], outputStr, 128) < 0)
		{
			cout << "Can't read a string!" << endl;
			exit(-1); 
		}
		
		cout << "\nParent's string in child process: " << outputStr << endl;
		close(pipe1[0]);
		
		if (write(pipe2[1], childStr, childStrSize) != childStrSize)
		{
			cout << "Can't write whole string!" << endl;
			exit(-1);
		}
		close(pipe2[1]);
		cout << "Child process terminated" << endl;
	}
	
	return 0;
	
}
