
#include <iostream>
using namespace std;

struct first_list
{
    int m;
    first_list* p;
};

void getm(int k, first_list* q)
{
    int i;
    first_list* t, * t1;
    t = q;
    for (i = 1; i < k; i++)
    {
        t1 = t->p;
        t = t1;
    }
    cout << "\nValue is " << t->m;
}

int main()
{
    int i, k, n;
    first_list *q0, *q1, *q2;
    q0 = new first_list;
    q1 = q0;
    cout << "\nEnter n: ";
    cin >> n;
    for (i = 1; i < n; i++)
    {
        cout << "\nValue m = ";
        cin >> q1->m;
        q2 = new first_list;
        q1->p = q2;
        q1 = q2;
    }
    cout << "\nValue m = ";
    cin >> q1->m;
    q1->p = q0;
    do 
    {
        cout << "\nValue for index k = ";
        cin >> k;
        if (!k)
        {
            for (i = 1; i <= n; i++)
            {
                q1 = q2->p;
                delete q2;
                q2 = q1;
            }
            return 0;
        }
        getm(k, q0);
    } while (true);
}

